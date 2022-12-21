using DG.Tweening;
using UnityEngine;
using Utils;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager: MonoSingleton<SoundManager>
    {
        [SerializeField] private AudioClip bso;
        [SerializeField] private AudioClip cutEffect;
        [SerializeField] private AudioClip perfectEffect;
        [SerializeField] private AudioClip restartEffect;

        private AudioSource _audioSource;
        
        
        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.clip = bso;
            _audioSource.Play();
        }

        public void PlayNormal() =>
            _audioSource.PlayOneShot(cutEffect);
        
        public void PlayPerfect() =>
            _audioSource.PlayOneShot(perfectEffect);
        
        public void PlayButton() =>
            _audioSource.PlayOneShot(restartEffect);

        public void PitchValue(float delta) =>
            _audioSource.pitch += delta;

        public void ResetPitch()
        {
            if (_audioSource.pitch > 1)
                _audioSource.DOPitch(1, 0.3f);
        }
    }
}
