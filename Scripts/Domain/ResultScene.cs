using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unlocks.View;

namespace Unlocks.Domain
{
    public class ResultScene : MonoBehaviour
    {
        [SerializeField]
        private ResultAnimation resultAnimation;

        [SerializeField]
        private StartButtonCover buttonCover;

        private async void Start()
        {
            var time = GameManager.Instance.Status.GetTime().Value;
            resultAnimation.Initialize(time);

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await resultAnimation.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            var timeSpan = TimeSpan.FromSeconds(time);
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timeSpan);

            SceneManager.sceneUnloaded += OnCloseRankingBoard;
        }

        private async void OnCloseRankingBoard(Scene scene)
        {
            if (scene.name != "Ranking") return;
            SceneManager.sceneUnloaded -= OnCloseRankingBoard;

            buttonCover.Open().Forget();
        }

        public async void OpenTitle()
        {
            await GameManager.Instance.Transition.Open();

            await SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);

            GameManager.Instance.Transition.Close().Forget();
        }
    }
}