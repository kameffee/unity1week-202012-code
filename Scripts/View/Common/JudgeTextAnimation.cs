using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Unlocks.View.Common
{
    public class JudgeTextAnimation : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI errorText;

        [SerializeField]
        private TextMeshProUGUI successText;

        [SerializeField]
        private UnityEvent onSuccess;

        [SerializeField]
        private UnityEvent onError;

        private void Awake()
        {
            errorText.DOFade(0, 0);
            successText.DOFade(0, 0);
        }

        /// <summary>
        /// エラー点滅表示後に消える
        /// </summary>
        public async void ErrorFlash()
        {
            onError.Invoke();

            await errorText.DOFade(1, 0.5f).SetEase(Ease.Flash, 9);
            errorText.DOFade(0, 0.1f).SetDelay(1);
        }

        public async void SuccessFlash()
        {
            onSuccess.Invoke();

            await successText.DOFade(1, 0.5f).SetEase(Ease.Flash, 6);
            successText.DOFade(1, 0.1f);
        }
    }
}