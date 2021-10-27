using UnityEngine;

namespace DamageSystem
{
    public class AttackAnimHandler : MonoBehaviour
    {
        public void ActivateWeapon()
        {
            transform.parent.GetComponent<PlayerHealth>().weapon.WeaponActive = true;
        }

        public void DeactivateWeapon()
        {
            transform.parent.GetComponent<PlayerHealth>().weapon.WeaponActive = false;
        }
    }
}