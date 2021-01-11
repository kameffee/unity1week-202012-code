using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unlocks.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Unlocks.Domain
{
    /// <summary>
    /// ステージの構築
    /// </summary>
    public class StageManager
    {
        private readonly string[] stageNames =
        {
            "CardKey",
            "Switch",
            "Password",
            "TouchCard",
            "Gesture",
        };

        private List<string> stageList;

        public int CurrentIndex { get; set; } = 0;

        public Scene CurrentScene { get; set; }

        public void Initialize()
        {
            CurrentIndex = 0;
            stageList = stageNames.OrderBy(s => Guid.NewGuid()).ToList();
        }

        public async UniTask LoadScene(int addMeter = 1)
        {
            // 画面を覆う
            await GameManager.Instance.Transition.Open();

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            // メーターを表示
            await GameManager.Instance.StatusCanvas.Open();
            GameManager.Instance.Status.Add(addMeter);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            if (GameManager.Instance.Status.IsFinish())
            {
                // リザルト画面へ
                await SceneManager.LoadSceneAsync("Result");
            }
            else
            {
                string sceneName = GetSceneName();
                await GameManager.Instance.StatusCanvas.OpenNextTask();
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                await GameManager.Instance.StatusCanvas.CloseNextTask();

                await SceneManager.LoadSceneAsync(sceneName);

                CurrentScene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(CurrentScene);

                GameBaseManager baseManager = null;
                foreach (var rootGameObject in CurrentScene.GetRootGameObjects())
                {
                    if (rootGameObject.TryGetComponent<GameBaseManager>(out baseManager))
                    {
                        break;
                    }
                }

                Debug.Assert(baseManager != null, "Not found. GameBaseManager");

                baseManager.OnSuccessAsObservable()
                    .Subscribe(_ => OnSuccessStage())
                    .AddTo(baseManager);
            }

            await GameManager.Instance.StatusCanvas.Close();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            await GameManager.Instance.Transition.Close();
        }

        private void OnSuccessStage()
        {
            CurrentIndex++;
            NextStage();
        }

        public async void NextStage()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));

            LoadScene(2).Forget();
        }

        private string GetSceneName()
        {
            return stageList[CurrentIndex];
        }
    }
}