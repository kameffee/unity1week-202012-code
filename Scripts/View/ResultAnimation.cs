using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Unlocks.View
{
    public class ResultAnimation : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI resultText;

        [SerializeField]
        private TextMeshProUGUI timeText;

        [SerializeField]
        private TextMeshProUGUI congText;

        private float _time;

        public void Initialize(float time)
        {
            _time = time;
        }

        public async UniTask Play()
        {
            await resultText.DOText("Mission.......Clear", 2f)
                .SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await timeText.DOText($"Time... {_time}s", 1f);

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await congText.DOText("Congratulations... :)", 2f);
        }
    }
}