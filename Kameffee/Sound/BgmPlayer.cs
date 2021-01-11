using UnityEngine;

namespace Kameffee.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class BgmPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.playOnAwake = false;
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Play(AudioClip clip, bool isLoop = true)
        {
            _audioSource.clip = clip;
            _audioSource.loop = isLoop;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }

        public void Pause()
        {
            _audioSource.Pause();
        }
    }
}