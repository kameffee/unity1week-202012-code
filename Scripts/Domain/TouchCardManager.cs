using Unlocks.View.Common;
using Unlocks.View;
using UniRx;
using UnityEngine;

namespace Unlocks.Domain
{
    public class TouchCardManager : GameBaseManager
    {
        [SerializeField]
        private TouchCardArea touchCardArea;

        [SerializeField]
        private JudgeTextAnimation judgeTextAnimation;

        protected void Start()
        {
            touchCardArea.OnScan()
                .Subscribe(_ => Success());

            GameStart();
        }

        public override void OnSuccess()
        {
            judgeTextAnimation.SuccessFlash();

            base.OnSuccess();
        }
    }
}