using System.Collections;
using UnityEngine;

namespace DamageSystem
{
    public class AnimatedDamageable : Damageable
    {
        protected Animator Animator;
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int Damage = Animator.StringToHash("damage");

        protected override void OnDamageReceived(WeaponData source, float amount)
        {
            base.OnDamageReceived(source, amount);
            if(!IsDead)
                Animator.SetTrigger(Damage);
        }

        protected override void Init()
        {
            Animator = GetComponentInChildren<Animator>();
        }
        
        protected override void Dead()
        {
            Animator.SetTrigger(Death);
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(5);
            GameController.Instance.DestroyTickable(gameObject);
        }
    }
}