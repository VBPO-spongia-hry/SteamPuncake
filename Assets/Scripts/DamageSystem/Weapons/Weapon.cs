using UnityEngine;

namespace DamageSystem.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData weapon;
        
        private bool _weaponActive;
        private CapsuleCollider _collider;
        private TrailRenderer _trail;
        private void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
            _trail = GetComponentInChildren<TrailRenderer>();
            WeaponActive = false;
        }

        public void SetTrailColor(Color color) => SetTrailColor(color, color);
        
        public void SetTrailColor(Color startColor, Color endColor)
        {
            _trail.startColor = startColor;
            _trail.endColor = endColor;
        }

        public bool WeaponActive
        {
            get => _weaponActive;
            set
            {
                _weaponActive = value;
                if (_trail)
                {
                    _trail.enabled = value;
                    _trail.Clear();
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
                    if (!weapon.splashDamage)
                        WeaponActive = false;
                    Debug.Log("hit: "+ other);
                    owner.SendDamage(weapon, damageable, weapon.baseDamage);
                }
            }            
        }
    }
}