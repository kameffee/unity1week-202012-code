using System;
using UnityEngine;

namespace Kameffee.Sound
{
    [Serializable]
    public sealed class SeData
    {
        [SerializeField]
        private string key;

        public string Key => key;

        [SerializeField]
        private AudioClip audioClip;

        public AudioClip AudioClip => audioClip;
    }
}