using System.Collections.Generic;
using UnityEngine;

namespace Kameffee.Sound
{
    public sealed class SePlayer : MonoBehaviour
    {
        private int initialCount = 3;

        private List<AudioSource> _players = new List<AudioSource>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            for (int i = 0; i < initialCount; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                _players.Add(source);
            }
        }

        protected AudioSource FindPlayableAudioSource()
        {
            AudioSource playablePlayer = null;
            foreach (var player in _players)
            {
                if (!player.isPlaying)
                {
                    playablePlayer = player;
                    break;
                }
            }

            // 空きが無かった
            if (playablePlayer == null)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                _players.Add(source);

                playablePlayer = source;
            }

            return playablePlayer;
        }

        /// <summary>
        /// 効果音の再生
        /// </summary>
        public void Play(AudioClip audioClip)
        {
            // 再生していないプレイヤーを探す
            var playablePlayer = FindPlayableAudioSource();
            playablePlayer.PlayOneShot(audioClip);
        }

        public void SetVolume(float volume)
        {
            foreach (var audioSource in _players)
            {
                audioSource.volume = volume;
            }
        }
    }
}