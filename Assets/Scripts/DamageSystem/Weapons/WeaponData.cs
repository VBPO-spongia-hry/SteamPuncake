using UnityEngine;

namespace DamageSystem.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public float baseDamage;
        public float rechargeTime;
        public bool splashDamage;
    }
}