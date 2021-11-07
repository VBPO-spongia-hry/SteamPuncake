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
                canvas.transform.rotation = Quaternion.Euler(-canvas.transform.eulerAngles.x, 0,0);
            }
            
        }

        public virtual void Init(float maxHealth)
        {
            _health = maxHealth;
            healthSlider.maxValue = _health;
            healthSlider.value = _health;
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