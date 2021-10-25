using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DamageSystem
{
    public class PlayerHealth : AnimatedDamageable
    {
        public Weapon weapon;
        private bool _fighting;
        private static readonly int Fighting = Animator.StringToHash("Fighting");
        
        private IEnumerator Attack()
        {
            _fighting = true;
            weapon.WeaponActive = true;
            Animator.SetTrigger(Fighting);
            yield return new WaitForSeconds(weapon.weapon.rechargeTime / 2);
            weapon.WeaponActive = false;
            yield return new WaitForSeconds(weapon.weapon.rechargeTime / 2);
            _fighting = false;
        }
        
        private void Update()
        {
            if (!PlayerMovement.DisableInput && !_fighting)
            {
                if (Input.GetButtonDown("Fire1")) StartCoroutine(Attack());
            }
        }
    }
}