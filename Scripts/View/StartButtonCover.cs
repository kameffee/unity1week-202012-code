using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unlocks.View
{
    public class StartButtonCover : MonoBehaviour
    {
        [SerializeField]
        private RectTransform left;

        [SerializeField]
        private RectTransform right;

        public async UniTask Open()
        {
            left.DOAnchorPosX(0, 1);
            right.DOAnchorPosX(0, 1);
        }
    }
}