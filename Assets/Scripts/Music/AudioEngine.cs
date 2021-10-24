using System;
using UnityEngine;

namespace Music
{
    public class AudioEngine : MonoBehaviour
    {
        public static float SpectrumValue { get; private set; }
        public static float Volume;
        private float[] _audioSpectrum;

        private void Start()
        {
            _audioSpectrum = new float[128];
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(_audioSpectrum, 0, FFTWindow.Hamming);
            if (_audioSpectrum != null && _audioSpectrum.Length > 0)
            {
                SpectrumValue = _audioSpectrum[0] * 100;
                float volume = 0;
                foreach (var sample in _audioSpectrum)
                {
                    volume += Mathf.Abs(sample);
                }

                // volume /= 128;
                Volume = volume;
            }
        }
    }
}