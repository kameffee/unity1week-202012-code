using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Unlocks.View
{
    public class HelpTextAnimation : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private Tween tween;
        
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.DOFade(0, 0);
        }

        public async void Play()
        {
            tween = text.DOFade(1, 0.5f).SetEase(Ease.Flash, 7);
            await tween;
            tween = text.DOFade(0, 0.1f).SetDelay(1);
            await tween;
        }

        public void Stop()
        {
            text.DOFade(0, 0.1f);
            if (tween != null && tween.IsPlaying())
            {
                tween.Kill();
            }
        }
    }
}