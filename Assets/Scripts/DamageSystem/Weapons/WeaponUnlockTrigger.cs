using System;
using UnityEngine;

namespace DamageSystem.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class WeaponUnlockTrigger : MonoBehaviour
    {
        public GameObject weapon;
        public Transform weaponSlot;
        
        private void Start()
        {
            if (WeaponSelect.UnlockedWeapons.IsUnlocked(weapon.name))
            {
                Destroy(gameObject);
                return;
            }
            
            Instantiate(weapon, weaponSlot);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Destroy(gameObject);
            WeaponSelect.UnlockedWeapons.Unlock(weapon.name);
        }
    }
}