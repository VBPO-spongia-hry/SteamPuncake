using System;
using UnityEngine;

namespace DamageSystem
{
    public abstract class Damageable : MonoBehaviour
    {
        private void Start()
        {
            _health = maxHealth;
            Init();
        }

        protected virtual void OnDamageReceived(WeaponData source, float amount)
        {
            ApplyDamage(amount);
        }

        protected void ApplyDamage(float amount)
        {
            _health -= amount;
            if (_health <= 0)
            {
                Dead();
            }
        }

        protected abstract void Init();
        protected abstract void Dead();

        public virtual void SendDamage(WeaponData weapon, Damageable target)
        {
            target.OnDamageReceived(weapon, weapon.baseDamage);
        }
        private float _health;
        [SerializeField]
        protected float maxHealth;
        
        
    }
}