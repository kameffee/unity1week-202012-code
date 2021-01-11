using System;
using UnityEngine;

namespace Kameffee.Sound
{
    public class PlayBgm : MonoBehaviour
    {
        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        private bool isAutoPlay = false;

        [SerializeField]
        public bool isLoop = true;

        private void Start()
        {
            if (isAutoPlay)
            {
                Play();
            }
        }

        public void Play()
        {
            Play(audioClip);
        }

        public void Play(AudioClip clip)
        {
            BgmManager.Instance.Play(clip);
        }

        public void Stop()
        {
            BgmManager.Instance.Stop();
        }
    }
}