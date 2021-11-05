using UnityEngine;

namespace Music
{
    public abstract class BaseSyncer : MonoBehaviour
    {
        [Tooltip("Spectrum Value that Triggers Event")]
        public float bias;
        [Tooltip("Time between 2 beat Events")]
        public float timeStep;
        [Tooltip("How fast will it transform")]
        public float timeToBeat; 
        public float restSmoothTime;
        
        private float _previousAudioValue;
        private float _audioValue;
        private float _timer;

        protected bool IsBeat;
        
        public virtual void OnBeat()
        {
            _timer = 0;
            IsBeat = true;
        }
        
        public virtual void Update()
        {
            // update audio value
            _previousAudioValue = _audioValue;
            _audioValue = AudioEngine.SpectrumValue;

            // if audio value went below the bias during this frame
            if (_previousAudioValue > bias && _audioValue <= bias)
            {
                // if minimum beat interval is reached
                if (_timer > timeStep)
                {
                    OnBeat();
                }
            }

            // if audio value went above the bias during this frame
            if (_previousAudioValue <= bias && _audioValue > bias)
            {
                // if minimum beat interval is reached
                if (_timer > timeStep)
                {
                    OnBeat();
                }
            }

            _timer += Time.deltaTime;
        }
    }
}