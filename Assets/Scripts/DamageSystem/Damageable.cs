using System;
using UnityEngine;

namespace DamageSystem
{
    public abstract class Damageable : MonoBehaviour
    {
        private HealthBar _healthBar;
        private void Start()
        {
            _health = maxHealth;
            Init();
            var healthBar = GetComponentInChildren<HealthBar>();
            if (healthBar)
                _healthBar = healthBar;

            if (_healthBar)
            {
                _healthBar.Init(maxHealth);                
            }
        }

        protected virtual void OnDamageReceived(WeaponData source, float amount)
        {
            if(_healthBar)
                _healthBar.OnDamage(amount);
            ApplyDamage(amount);
        }

        private void ApplyDamage(float amount)
        {
            _health -= amount;
            if (_health <= 0)
            {
                IsDead = true;
                Dead();
            }
        }

        protected abstract void Init();
        protected abstract void Dead();

        public virtual void SendDamage(WeaponData weapon, Damageable target)
        {
            if(!target.IsDead)
                target.OnDamageReceived(weapon, weapon.baseDamage);
        }
        private float _health;
        [SerializeField]
        protected float maxHealth;
        [NonSerialized]
        public bool IsDead;

    }
}