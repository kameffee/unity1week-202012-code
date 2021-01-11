using System.Collections.Generic;
using UnityEngine;

namespace Kameffee.Sound
{
    public class BgmManager : MonoBehaviour
    {
        #region Singleton

        private static BgmManager instance;

        public static BgmManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject("BgmManager");
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<BgmManager>();
                }

                return instance;
            }
        }

        #endregion

        private const int InitialInstance = 1;

        private List<BgmPlayer> playerList = new List<BgmPlayer>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            Instance.Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < InitialInstance; i++)
            {
                var obj = new GameObject("BGMPlayer");
                obj.transform.SetParent(transform);
                var player = obj.AddComponent<BgmPlayer>();
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
            foreach (var player in playerList)
            {
                player.Stop();
            }
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