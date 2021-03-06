using System;
using DamageSystem.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace DamageSystem
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField]
        private HealthBar healthBar;
        [SerializeField] 
        private GameObject hitEffect;
        private void Start()
        {
            _health = maxHealth;
            Init();
            var bar = GetComponentInChildren<HealthBar>();
            if (bar)
                healthBar = bar;

            if (healthBar)
            {
                healthBar.Init(maxHealth);                
            }
        }

        protected virtual void OnDamageReceived(WeaponData source, float amount)
        {
            if(healthBar)
                healthBar.OnDamage(amount);
            if (hitEffect)
            {
                var hit = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(hit, 2);    
            }
            ApplyDamage(amount);
        }

        private void ApplyDamage(float amount)
        {
            _health -= amount;
            if (_health <= 0)
            {
                IsDead = true;
                Dead();
                if (healthBar)
                {
                    healthBar.gameObject.SetActive(false);
                }
            }
        }

        protected abstract void Init();
        protected abstract void Dead();

        public virtual void SendDamage(WeaponData weapon, Damageable target, float damageAmmount)
        {
            if(!target.IsDead)
                target.OnDamageReceived(weapon, damageAmmount);
        }
        private float _health;
        [SerializeField]
        protected float maxHealth;
        [NonSerialized]
        public bool IsDead;

        public void Heal()
        {
            _health = maxHealth;
            if (healthBar)
            {
                healthBar.Init(maxHealth);
            }
        }
    }
}