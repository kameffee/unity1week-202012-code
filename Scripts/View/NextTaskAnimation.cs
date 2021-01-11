using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unlocks.View
{
    public class NextTaskAnimation : MonoBehaviour
    {
        [SerializeField]
        private Image frame;

        [SerializeField]
        private TextMeshProUGUI mainText;

        private void Awake()
        {
            frame.fillAmount = 0;
            mainText.DOFade(0, 0);
        }

        public async UniTask Open()
        {
            var tmp = mainText.text;
            mainText.text = "";
            mainText.DOFade(1, 0);
            await mainText.DOText(tmp, 0.5f);
            await frame.DOFillAmount(1, 1f);
            mainText.DOFade(0, 0);
            await mainText.DOFade(1f, 0.5f)
                .SetEase(Ease.Flash, 5);
        }

        public async UniTask Close()
        {
            mainText.DOFade(0, 0.5f);
            await frame.DOFade(0, 0.5f);
            frame.fillAmount = 0;
            frame.DOFade(1, 0);
        }
    }
}