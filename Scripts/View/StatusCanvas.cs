using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unlocks.Domain;
using Unlocks.View.Progress;
using TMPro;
using UniRx;
using UnityEngine;

namespace Unlocks.View
{
    public class StatusCanvas : MonoBehaviour
    {
        public static async UniTask<StatusCanvas> Load()
        {
            var request = Resources.LoadAsync<StatusCanvas>("UI/StatusCanvas");
            await request;

            return request.asset as StatusCanvas;
        }

        public static async UniTask<StatusCanvas> Create()
        {
            var asset = await Load();
            return Instantiate(asset);
        }

        [SerializeField]
        private CanvasGroup rootCanvas;

        [SerializeField]
        private ProgressMeter progressMeter;

        [SerializeField]
        private NextTaskAnimation nextTaskAnimation;

        [SerializeField]
        private TextMeshProUGUI progressText;

        private float percent = 0;

        private float Percent
        {
            get => percent;
            set
            {
                DOVirtual.Float(percent * 10, value * 10, 0.5f, v => progressText.text = $"{v:0}%");
                percent = value;
            }
        }

        private GameStatus _status;

        private void Start()
        {
            rootCanvas.alpha = 0;
        }

        public void Initialize(GameStatus status)
        {
            _status = status;
            progressMeter.Initialize(10, 0);
            _status.Progress()
                .Subscribe(progress =>
                {
                    var addValue = progress - progressMeter.Value;
                    progressMeter.Add(addValue);
                    Percent = progress;
                });
        }

        public async UniTask Open()
        {
            await rootCanvas.DOFade(1, 0.2f);
        }

        public async UniTask Close()
        {
            await rootCanvas.DOFade(0, 0.2f);
        }

        public async UniTask OpenNextTask()
        {
            await nextTaskAnimation.Open();
        }

        public async UniTask CloseNextTask()
        {
            await nextTaskAnimation.Close();
        }
    }
}