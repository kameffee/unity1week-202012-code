using Unlocks.View;
using Unlocks.View.Common;
using UniRx;
using UnityEngine;

namespace Unlocks.Domain
{
    public class CardKeyGameManager : GameBaseManager
    {
        [SerializeField]
        private CardScanner scanner;

        [SerializeField]
        private JudgeTextAnimation judgeTextAnimation;

        protected void Start()
        {
            scanner.OnResult()
                .Subscribe(result =>
                {
                    if (result)
                    {
                        Success();
                    }
                    else
                    {
                        judgeTextAnimation.ErrorFlash();
                    }
                });
            
            GameStart();
        }

        public override void OnSuccess()
        {
            judgeTextAnimation.SuccessFlash();

            base.OnSuccess();
        }
    }
}