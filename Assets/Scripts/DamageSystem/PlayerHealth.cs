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

        protected override void OnDamageReceived(WeaponData source, float amount)
        {
            
            base.OnDamageReceived(source, amount);
        }

        public override void SendDamage(WeaponData weapon, Damageable target)
        {
            
            base.SendDamage(weapon, target);
        }

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

        protected override void Dead()
        {
            base.Dead();
            PlayerMovement.DisableInput = true;
            GetComponent<PlayerMovement>().enabled = false;
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