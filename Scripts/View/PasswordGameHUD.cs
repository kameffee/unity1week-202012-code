using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unlocks.View.Common;
using TMPro;
using UnityEngine;

namespace Unlocks.View
{
    public class PasswordGameHUD : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI passwordText;

        [SerializeField]
        private JudgeTextAnimation judgeTextAnimation;

        [SerializeField]
        private TextMeshProUGUI hintText;

        private void Awake()
        {
            passwordText.text = string.Empty;
        }

        /// <summary>
        /// エラー点滅表示後に消える
        /// </summary>
        public async void ErrorFlash()
        {
            judgeTextAnimation.ErrorFlash();
        }

        public async void SuccessFlash()
        {
            judgeTextAnimation.SuccessFlash();
        }

        public void SetHintText(string text)
        {
            hintText.text = text;
        }

        public void ClearNumber()
        {
            passwordText.text = string.Empty;
        }

        public void UpdateInputView(string number)
        {
            passwordText.text = number;
        }
    }
}