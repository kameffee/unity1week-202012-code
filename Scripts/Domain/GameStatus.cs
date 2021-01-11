using System;
using UniRx;
using UnityEngine;

namespace Unlocks.Domain
{
    public class GameStatus
    {
        public readonly float ProgressMax = 10f;

        private ReactiveProperty<int> progress = new ReactiveProperty<int>(0);

        public IReadOnlyReactiveProperty<int> Progress() => progress;

        private ReactiveProperty<float> _time = new ReactiveProperty<float>(0);

        public IReadOnlyReactiveProperty<float> GetTime() => _time;

        public bool IsFinish() => progress.Value >= ProgressMax;

        private IDisposable timeCountDisposable;

        public void Add(int value)
        {
            progress.Value += value;
        }

        public void StartTimeCount()
        {
            timeCountDisposable = Observable.EveryUpdate()
                .Subscribe(_ => _time.Value += Time.deltaTime);
        }

        public void StopTimeCount()
        {
            Debug.Log("Stop: " + _time.Value);
            timeCountDisposable?.Dispose();
        }

        public void Reset()
        {
            progress.Value = 0;
            _time.Value = 0;
        }
    }
}