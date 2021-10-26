using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
        public bool IsInCombatMode => _target && !_target.IsDead && !IsDead;
        private EnemyMovement _movement;
        private NavMeshAgent _agent;
        private Damageable _target;
        private int _currentTick = 0;
        private static readonly int Fighting = Animator.StringToHash("attack");
        
        private void Attack()
        {
            Animator.SetTrigger(Fighting);
            SendDamage(weapon, _target);
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
                
                _target = other.GetComponent<Damageable>();
                Debug.Log("can see player: " + _target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("player escaped");
                _target = null;
            }
        }

        protected override void Init()
        {
            _movement = GetComponent<EnemyMovement>();
            _agent = GetComponent<NavMeshAgent>();
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