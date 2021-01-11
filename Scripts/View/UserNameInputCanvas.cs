using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unlocks.Domain;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Unlocks.View
{
    public class UserNameInputCanvas : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TMP_InputField inputField;

        [SerializeField]
        private Button submitButton;

        [SerializeField]
        private UnityEvent onSubmit;

        public IObservable<Unit> OnSubmit() => onSubmit.AsObservable();

        private UserModel model;

        public void Initialize(bool visible)
        {
            model = GameManager.Instance.User;

            if (visible)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            var userName = string.IsNullOrEmpty(model.UserName)
                ? $"Device{Random.Range(0, 10000):0000}"
                : model.UserName;
            inputField.text = userName;
        }

        public void SetUserName(string userName)
        {
            var input = userName.Trim();
            if (string.IsNullOrEmpty(input))
            {
                submitButton.interactable = false;
                return;
            }

            submitButton.interactable = true;
            model.UserName = input;
        }

        public async void Submit()
        {
            model.Save();
            onSubmit.Invoke();
        }

        public async void Close()
        {
            await canvasGroup.DOFade(0, 0.2f);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}