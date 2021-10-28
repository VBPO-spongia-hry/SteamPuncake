using System;
using UnityEngine;

namespace DamageSystem
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData weapon;
        public GameObject hitEffect;
        
        private bool _weaponActive;
        private CapsuleCollider _collider;

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
        }

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
                    if (hitEffect)
                    {
                        var start = transform.position + new Vector3(0, _collider.height / 2f) + _collider.center;
                        var end = transform.position - new Vector3(0, _collider.height / 2f) + _collider.center;
                        if (Physics.CapsuleCast(start, end, _collider.radius, transform.forward, out var hit))
                        {
                            var go = Instantiate(hitEffect, hit.point, Quaternion.Euler(hit.normal));
                            Destroy(go, 1);
                        }
                    }
                }
            }            
        }
    }
}