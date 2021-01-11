using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unlocks.View;
using Unlocks.View.Common;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Unlocks.Domain
{
    public class TitleScene : MonoBehaviour
    {
        [Header("NameInput")]
        [SerializeField]
        private UserNameInputCanvas nameInput;

        [Header("Intro")]
        [SerializeField]
        private IntroAnimation intro;

        [Header("Title")]
        [SerializeField]
        private CanvasGroup titleCanvas;

        [SerializeField]
        private HelpTextAnimation helpAnimation;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private TextMeshProUGUI unText;

        [SerializeField]
        private TextMeshProUGUI sText;

        [SerializeField]
        private JudgeTextAnimation judgeAnimation;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private StartButtonCover startButtonCover;

        private IDisposable slideHelpDisposable;

        private async void Start()
        {
            var isShowNameInput = string.IsNullOrEmpty(GameManager.Instance.User.UserName);
            nameInput.Initialize(isShowNameInput);
            if (isShowNameInput)
            {
                await nameInput.OnSubmit().ToUniTask(true);
                nameInput.Close();
            }

            GameManager.Instance.Status.Reset();

            startButton.interactable = false;
            startButton.gameObject.SetActive(false);
            titleCanvas.DOFade(0, 0);
            titleCanvas.interactable = false;

            await intro.Play();
            await intro.Close();
            OpenTitle();
        }

        public async void SkipIntro()
        {
            await intro.Close();
            OpenTitle();
        }

        public async void OpenTitle()
        {
            // 手を離したときに限界まで行っていなかったら戻す
            await titleCanvas.DOFade(1f, 0.5f);
            titleCanvas.interactable = true;
            startButton.gameObject.SetActive(true);

            slider.OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    if (slider.normalizedValue >= 1 - 0.01f)
                    {
                        // 成功
                        Success();
                    }
                    else
                    {
                        // 失敗
                        // 戻す
                        slider.DOValue(0, 0.25f);

                        judgeAnimation.ErrorFlash();
                    }
                });

            slideHelpDisposable = Observable.Interval(TimeSpan.FromSeconds(5f))
                .Subscribe(_ => { helpAnimation.Play(); }).AddTo(this);
        }

        public async void Success()
        {
            slideHelpDisposable?.Dispose();

            helpAnimation.Stop();

            slider.DOValue(1f, 0.1f);
            unText.DOFade(1, 1f);
            sText.DOFade(1, 1f);
            slider.enabled = false;
            slider.GetComponent<ButtonText>().SetActive(false);

            judgeAnimation.SuccessFlash();

            await UniTask.Delay(TimeSpan.FromSeconds(2f));

            await startButtonCover.Open();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            startButton.interactable = true;
        }

        public void GameStart()
        {
            Debug.Log("GameStart");
            GameManager.Instance.StageManager.Initialize();
            GameManager.Instance.StageManager.LoadScene(0).Forget();
        }
    }
}