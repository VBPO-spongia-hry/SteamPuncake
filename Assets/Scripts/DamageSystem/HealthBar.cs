using System;
using UnityEngine;
using UnityEngine.UI;

namespace DamageSystem
{
    public class HealthBar : MonoBehaviour
    {
        public Canvas canvas;
        public Slider healthSlider;

        private float _health;
        protected virtual void Update()
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, _health, Time.deltaTime);
            if (canvas)
            {
                canvas.transform.LookAt(Camera.main.transform, Vector3.up);    
            }
            
        }

        public virtual void Init(float maxHealth)
        {
            _health = maxHealth;
            healthSlider.value = _health;
            healthSlider.maxValue = _health;
            if (canvas)
                canvas.gameObject.SetActive(false);
        }

        public void OnDamage(float amount)
        {
            if(canvas)
                canvas.gameObject.SetActive(true);
            _health -= amount;
        }
    }
}