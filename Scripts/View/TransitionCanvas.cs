using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unlocks.View
{
    public interface ITransition
    {
        UniTask Open();

        UniTask Close();
    }

    public class TransitionCanvas : MonoBehaviour, ITransition
    {
        public static async UniTask<TransitionCanvas> Load()
        {
            var request = Resources.LoadAsync<TransitionCanvas>("UI/TransitionCanvas");
            await request;

            return request.asset as TransitionCanvas;
        }

        public static async UniTask<TransitionCanvas> Create()
        {
            var asset  = await Load();
            return Instantiate(asset);
        } 

        [SerializeField]
        private CanvasGroup rootCanvas;

        [SerializeField]
        private RectTransform up;

        [SerializeField]
        private RectTransform down;

        private void Start()
        {
            up.DOAnchorPosY(up.rect.height, 0);
            down.DOAnchorPosY(-down.rect.height, 0);
        }

        public async UniTask Open()
        {
            up.DOAnchorPosY(-10f, 1f)
                .SetEase(Ease.OutCirc);
            await down.DOAnchorPosY(10f, 1f)
                .SetEase(Ease.OutCirc);
        }

        public async UniTask Close()
        {
            up.DOAnchorPosY(up.rect.height, 0.5f);
            await down.DOAnchorPosY(-down.rect.height, 0.5f);
        }
    }
}