using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DamageSystem
{
    public class PlayerHealthBar : HealthBar
    {
        public Volume volume;
        private Vignette _vignette;
        protected override void Update()
        {
            base.Update();
            if (_vignette)
            {
                _vignette.intensity.value = (1 - healthSlider.value / healthSlider.maxValue) / 2f;
            }
        }

        public override void Init(float maxHealth)
        {
            base.Init(maxHealth);
            volume.profile.TryGet(out _vignette);
        }
    }
}