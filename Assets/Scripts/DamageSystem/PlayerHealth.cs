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

        // This method is called whenever a player receives damage
        // use it to calculate damage amount, that player receives
        protected override void OnDamageReceived(WeaponData source, float amount)
        {
            //tu si pisem ja
            float amount= source.baseDamage


            base.OnDamageReceived(source, amount);
        }

        // This method is called when player hits something. Use it to calculate how much damage does
        // make using current combo
        public override void SendDamage(WeaponData weapon, Damageable target)
        {
            //tu si pisem ja
            float damageAmmount= weapon.baseDamage;

            
            base.SendDamage(weapon, target, damageAmmount);
        }

        private IEnumerator Attack()
        {
            _fighting = true;
            Animator.SetTrigger(Fighting);
            yield return new WaitForSeconds(weapon.weapon.rechargeTime);
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