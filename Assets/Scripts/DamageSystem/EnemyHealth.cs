using System;
using UnityEngine;

namespace DamageSystem
{
    [Serializable]
    public enum RhythmCommandType {
        None = 0,
        Attack = 1,
        Block = 2,
        Escape = 3
    }
    public class EnemyHealth : AnimatedDamageable, ITickable
    {
        public RhythmCommandType[] rhythm;
        public WeaponData weapon;
        public bool IsInCombatMode => Target && !Target.IsDead && !IsDead;
        private EnemyMovement _movement;
        public Damageable Target { get; private set; }
        private int _currentTick = 0;
        private static readonly int Fighting = Animator.StringToHash("attack");
        
        private void Attack()
        {
            Animator.SetTrigger(Fighting);
            SendDamage(weapon, Target, weapon.baseDamage);
        }

        public void OnGameTick()
        {
            if (!IsInCombatMode) return;
            CombatTick(rhythm[_currentTick]);
            _currentTick++;
            if (_currentTick >= rhythm.Length)
                _currentTick = 0;
        }

        private void CombatTick(RhythmCommandType action)
        {
            switch (action)
            {
                case RhythmCommandType.None:
                    break;
                case RhythmCommandType.Attack:
                    Attack();
                    break;
                case RhythmCommandType.Block:
                    break;
                case RhythmCommandType.Escape:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                Target = other.GetComponent<Damageable>();
                Debug.Log("can see player: " + Target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("player escaped");
                Target = null;
            }
        }

        protected override void Init()
        {
            _movement = GetComponent<EnemyMovement>();
            base.Init();
        }

        protected override void Dead()
        {
            _movement.enabled = false;
            base.Dead();
        }

        public void OnSpawn()
        {
        }
    }
}