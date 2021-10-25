using System;
using UnityEngine;

namespace DamageSystem
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData weapon;
        [NonSerialized]
        public bool WeaponActive = false;
        public Damageable owner;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("hit: "+ other);
            if (WeaponActive)
            {
                var damageable = other.GetComponent<Damageable>();
                if (damageable && damageable != owner)
                {
                    owner.SendDamage(weapon, damageable);
                }
            }            
        }
    }
}