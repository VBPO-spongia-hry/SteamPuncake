using UnityEngine;

namespace DamageSystem
{
    public class EnemyHealth : AnimatedDamageable
    {
        public WeaponData weaponData;
        public override void SendDamage(WeaponData weapon, Damageable target)
        {
            base.SendDamage(weapon, target);
        }

        protected override void Init()
        {
               
        }
    }
}