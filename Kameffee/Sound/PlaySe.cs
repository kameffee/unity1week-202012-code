using UnityEngine;

namespace Kameffee.Sound
{
    public class PlaySe : MonoBehaviour
    {
        [SerializeField]
        private AudioClip audioClip;

        public void Play()
        {
            Play(audioClip);
        }

        public void Play(AudioClip clip)
        {
            SeManager.Instance.Play(clip);
        }

        public void Stop()
        {
            SeManager.Instance.Stop();
        }
    }
}