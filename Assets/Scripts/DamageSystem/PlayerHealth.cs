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

        //"plnenie" comba
        //public int comboprogress;

        // This method is called whenever a player receives damage
        // use it to calculate damage amount, that player receives
        
        protected override void OnDamageReceived(WeaponData source, float amount)
        {
            //tu si pisem ja
            /*float amount= source.baseDamage;
            if (comboprogress > 0){
                comboprogress--;
            }
            else if(combo>0){
                combo--;
            }
            }
            */
            base.OnDamageReceived(source, amount);
        }

        // This method is called when player hits something. Use it to calculate how much damage does
        // make using current combo
        public override void SendDamage(WeaponData weapon, Damageable target, float damageAmmount)
        {
            //tu si pisem ja
            /*if (comboprogress < 5){
                comboprogress++;
            }
            else if(combo < 3){
                combo++;

            float[] multiplier = { 1, 1.5, 1.8, 2};

            //beatoffset = ako moc mimo beatu si, ak si presne tak je to 0, ak si uplne medzi tak je to 0.5
            if (_timer/_nextTick<0.5){
                float beatoffset= _timer/_nextTick;
            }
            else{
                float beatoffset= (_timer/_nextTick)-1;
            }
                
            //funkcia ti spravi, ze ak si uplne mimo, tak vynasobi tvoj damage *0, ak to relativne trafis
            //tak by to malo byt v okolo 1-0.8, pozri si v desmose funkciu -4*x^2 +1
            float timing=(-4*beatoffset^2)+1; 
            
            float damageAmmount= weapon.baseDamage*multiplier[combo]*timing;
            */
            
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