using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unlocks.View
{
    public class RankingBoardAnimation : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private RectTransform window;

        private void Awake()
        {
            canvasGroup.alpha = 0;
        }

        private async void Start()
        {
            window.DOAnchorPos(new Vector2(-100f, 50f), 0);

            for (int i = 0; i < 3; i++)
            {
                canvasGroup.alpha = 0;
                var x = Random.Range(-100f, 100f);
                var y = Random.Range(-100f, 100f);
                window.DOAnchorPos(new Vector2(x, y), 0);
                await canvasGroup.DOFade(1, 0.1f)
                    .SetEase(Ease.Flash, 3);;
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            
            canvasGroup.alpha = 0;
            window.DOAnchorPos(new Vector2(0f, 0f), 0);
            await canvasGroup.DOFade(1, 0.5f)
                .SetEase(Ease.OutFlash, 9);

        }
    }
}