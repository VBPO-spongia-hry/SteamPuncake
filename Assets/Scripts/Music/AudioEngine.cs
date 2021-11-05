using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Music
{
    public class AudioEngine : MonoBehaviour
    {
        public static float SpectrumValue { get; private set; }
        private static AudioEngine _instance;
        public static float Volume;
        public AudioMixer mixer;
        public AudioSource musicSource;
        private float[] _audioSpectrum;
        private float _speed = 1;
        
        private void Start()
        {
            _audioSpectrum = new float[128];
            _instance = this;
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
            
            SetTempo();
        }

        private void SetTempo()
        {
            musicSource.pitch = _speed;
            mixer.SetFloat("pitchBend", 1f / _speed);
        }

        public static void SetTempo(float multiplier)
        {
            _instance._speed = multiplier;
            _instance.SetTempo();
        }
    }
}