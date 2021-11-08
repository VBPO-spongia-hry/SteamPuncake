using System.Collections;
using DamageSystem.Weapons;
using UnityEngine;

namespace DamageSystem
{
    public class AnimatedDamageable : Damageable
    {
        public AudioClip attackClip;
        public AudioClip hitClip;
        protected Animator Animator;
        protected AudioSource AudioSource;
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int Damage = Animator.StringToHash("damage");

        protected override void OnDamageReceived(WeaponData source, float amount)
        {
            if (AudioSource && hitClip)
            {
                AudioSource.clip = hitClip;
                AudioSource.Play();
            }
            base.OnDamageReceived(source, amount);
            if(!IsDead)
                Animator.SetTrigger(Damage);
        }

        public override void SendDamage(WeaponData weapon, Damageable target, float damageAmmount)
        {
            if (AudioSource && attackClip)
            {
                AudioSource.clip = attackClip;
                AudioSource.Play();    
            }
            base.SendDamage(weapon, target, damageAmmount);
        }

        protected override void Init()
        {
            Animator = GetComponentInChildren<Animator>();
            AudioSource = GetComponent<AudioSource>();
        }
        
        protected override void Dead()
        {
            Animator.SetTrigger(Death);
            // GetComponent<Collider>().enabled = false;
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(5);
            GameController.Instance.DestroyTickable(gameObject);
        }
    }
}