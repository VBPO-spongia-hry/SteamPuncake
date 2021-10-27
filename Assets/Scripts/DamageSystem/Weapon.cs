using System;
using UnityEngine;

namespace DamageSystem
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData weapon;

        private bool _weaponActive;

        public bool WeaponActive
        {
            get => _weaponActive;
            set
            {
                _weaponActive = value;
                var trail = GetComponentInChildren<TrailRenderer>(); 
                if (trail)
                {
                    trail.enabled = value;
                    trail.Clear();
                }
            }
        }

        public Damageable owner;
        
        private void OnTriggerEnter(Collider other)
        {
            
            if (WeaponActive)
            {
                var damageable = other.GetComponent<Damageable>();
                if (damageable && damageable != owner)
                {
                    Debug.Log("hit: "+ other);
                    owner.SendDamage(weapon, damageable);
                }
            }            
        }
    }
}