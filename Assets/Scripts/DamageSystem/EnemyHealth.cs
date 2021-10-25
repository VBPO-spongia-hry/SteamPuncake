using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace DamageSystem
{
    
    public class EnemyHealth : AnimatedDamageable
    {
        public WeaponData weapon;

        private EnemyMovement _movement;
        private NavMeshAgent _agent;
        private bool _fighting;
        private Damageable _target;
        private static readonly int Fighting = Animator.StringToHash("attack");
        
        private IEnumerator Attack()
        {
            _fighting = true;
            Animator.SetTrigger(Fighting);
            SendDamage(weapon, _target);
            yield return new WaitForSeconds(weapon.rechargeTime);
            _fighting = false;
        }

        private void Update()
        {
            if (!_fighting && _target && !_target.IsDead)
            {
                StartCoroutine(Attack());
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
    }
}