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