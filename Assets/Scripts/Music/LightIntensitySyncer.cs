using System;
using UnityEngine;

namespace Music
{
    public class LightIntensitySyncer : MonoBehaviour
    {
        public float minIntensity;
        [Range(0,1)]
        public float strength;
        public float amplitude;
        private Light _light;
        private void Start()
        {
            _light = GetComponent<Light>();
        }

        private void Update()
        {
            _light.intensity = Mathf.Lerp(  _light.intensity, minIntensity + amplitude * AudioEngine.Volume, strength);
        }
    }
}