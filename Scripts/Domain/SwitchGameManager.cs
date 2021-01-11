using System.Collections.Generic;
using Unlocks.View;
using Unlocks.View.Common;
using UniRx;
using UnityEngine;

namespace Unlocks.Domain
{
    public class SwitchGameManager : GameBaseManager
    {
        [SerializeField]
        private List<GameObject> switchList = new List<GameObject>();

        [SerializeField]
        private JudgeTextAnimation judgeTextAnimation;

        [SerializeField]
        private List<GameObject> list = new List<GameObject>();

        protected void Start()
        {
            Setup();
            GameStart();
        }

        public void Setup()
        {
            Initialize(Random.Range(3, 9));
        }

        public void Initialize(int max)
        {
            foreach (var o in list)
            {
                o.SetActive(false);
            }

            // 表示するインデックスリスト
            var indexList = GetRandomIndex(0, list.Count - 1, max);
            foreach (var i in indexList)
            {
                var target = list[i];
                target.SetActive(true);

                if (target.TryGetComponent<ISwitch>(out var s))
                {
                    var isOn = max > 5 && Random.Range(0, 5) > 2;
                    s.Initialize(isOn);
                    s.OnChangedValue()
                        .Subscribe(value => Check())
                        .AddTo(target);
                }

                switchList.Add(target);
            }
        }

        public static IEnumerable<int> GetRandomIndex(int min, int max, int count)
        {
            var random = new System.Random();

            var indexList = new List<int>();
            for (int i = min; i <= max; i++) // 最大値を含む
            {
                indexList.Add(i);
            }

            for (int i = 0; i < count; i++)
            {
                int index = random.Next(0, indexList.Count);
                int value = indexList[index];
                indexList.RemoveAt(index);
                yield return value;
            }
        }

        public void Check()
        {
            if (!IsPlaying) return;
            
            Debug.Log("Check");
            if (Judge())
            {
                Success();
            }
            else
            {
                // judgeTextAnimation.ErrorFlash();
            }
        }

        public override void OnSuccess()
        {
            judgeTextAnimation.SuccessFlash();

            base.OnSuccess();
        }

        private bool Judge()
        {
            foreach (var switchObject in switchList)
            {
                if (switchObject.TryGetComponent<ISwitch>(out var s))
                {
                    if (!s.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}