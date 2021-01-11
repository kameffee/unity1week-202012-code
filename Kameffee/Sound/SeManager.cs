using System.Collections.Generic;
using UnityEngine;

namespace Kameffee.Sound
{
    public class SeManager : MonoBehaviour
    {
        #region Singleton

        private static SeManager instance;

        public static SeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject("BgmManager");
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<SeManager>();
                }

                return instance;
            }
        }

        #endregion

        private const int InitialInstance = 1;

        private List<SePlayer> playerList = new List<SePlayer>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            Instance.Initialize();
        }
        
        public void Initialize()
        {
            for (int i = 0; i < InitialInstance; i++)
            {
                var obj = new GameObject("SEPlayer");
                obj.transform.SetParent(transform);
                var player = obj.AddComponent<SePlayer>();
                playerList.Add(player);
            }
        }
        
        public void Play(string path)
        {
            var clip = Resources.Load<AudioClip>(path);
            Play(clip);
        }

        public void Play(AudioClip clip, bool isLoop = true)
        {
            playerList[0].Play(clip);
        }

        public void Stop()
        {
        }

        /// <param name="volume">0~1</param>
        public void SetVolume(float volume)
        {
            foreach (var player in playerList)
            {
                player.SetVolume(volume);
            }
        }
    }
}