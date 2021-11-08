using System;
using System.Collections;
using DamageSystem.Weapons;
using Levels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace DamageSystem
{
    public class PlayerHealth : AnimatedDamageable
    {
        public Weapon weapon;
        private bool _fighting;
        private static readonly int Fighting = Animator.StringToHash("Fighting");

        private float combo => GameController.Instance.combo;
        private float comboprogress => GameController.Instance.comboprogress;
        private void UpdateCombo(int comboChange){
            GameController.Instance.UpdateCombo(comboChange);
        }
        private void Updatecomboprogress(float comboprogressChange){
            GameController.Instance.Updatecomboprogress(comboprogressChange);
        }

        private float beatdistance => GameController.Instance.Beatdistance;
        public float beatoffset;
        [NonSerialized]
        public bool Blocking;

        // This method is called whenever a player receives damage
        // use it to calculate damage amount, that player receives
        protected override void OnDamageReceived(WeaponData source, float amount)
        {
            // TODO: Damage reduction if blocking
            amount= source.baseDamage;
            if (!Blocking)
            {
                /*if (comboprogress > 0)
                {
                    Updatecomboprogress(-1);
                }
                else if (combo > 0)
                {
                    UpdateCombo(-1);
                    Updatecomboprogress(4);
                }*/
                Updatecomboprogress(-1);
            }
            else
                amount /= 10;
            base.OnDamageReceived(source, amount);
        }

        // This method is called when player hits something. Use it to calculate how much damage does
        // make using current combo
        public override void SendDamage(WeaponData weapon, Damageable target, float damageAmmount)
        {
            //tu si pisem ja
            /*if (comboprogress < 4){
                Updatecomboprogress(1);
                //Debug.Log ("comboprogress:" + comboprogress);
            }
            else if(combo < 3){
                UpdateCombo(1);
                Updatecomboprogress(-4);
                //Debug.Log ("combo:" + combo);
            }*/
            
            float[] multiplier = { 1, 1.5f, 1.8f, 2};

            //beatoffset = ako moc mimo beatu si, ak si presne tak je to 0, ak si uplne medzi tak je to 0.5
           if (beatdistance <= 0.5){
               beatoffset=beatdistance;
           }
           else{
               beatoffset=1-beatdistance;
           }

           if (beatoffset > .3f)
           {
               GameController.Instance.Updatecomboprogress(-1);
           }
            //funkcia ti spravi, ze ak si uplne mimo, tak vynasobi tvoj damage *0, ak to relativne trafis
            //tak by to malo byt v okolo 1-0.8, pozri si v desmose funkciu -4*x^2 +1
            float timing=(-4*beatoffset*beatoffset)+1;
            
            Updatecomboprogress(timing); 
            
            damageAmmount= weapon.baseDamage*multiplier[GameController.Instance.combo]*timing;
            //Debug.Log ("dmg:" + damageAmmount);
            // update comba (zmenenie soudntracku a textury)
            // GameController.Instance.UpdateCombo(combo);
            base.SendDamage(weapon, target, damageAmmount);
            
        }

        private IEnumerator Attack()
        {
            _fighting = true;
            Animator.SetTrigger(Fighting);
            // camera shake: 
            // Camera.main.GetComponent<CameraShake>().Shake();
            // Hit trail color:
            // weapon.SetTrailColor(startColor, endColor);
            // or:
            // weapon.SetTrailColor(color);
            yield return new WaitForSeconds(weapon.weapon.rechargeTime);
            _fighting = false;
        }

        protected override void Dead()
        {
            PlayerMovement.DisableInput = true;
            LevelController.Instance.deathUI.SetActive(true);
            GetComponent<PlayerMovement>().enabled = false;
            base.Dead();
        }

        private void Update()
        {
            if (!PlayerMovement.DisableInput && !_fighting)
            {
                Blocking = Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space);
                Animator.SetBool("Blocking", Blocking);
                if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject() && !Blocking) StartCoroutine(Attack());
            }
        }
    }
}