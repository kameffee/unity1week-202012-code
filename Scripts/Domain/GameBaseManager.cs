using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Unlocks.Domain
{
    public class GameBaseManager : MonoBehaviour
    {
        public bool IsPlaying { get; protected set; } = false;

        public bool IsSuccess { get; protected set; } = false;

        #region Events

        [SerializeField]
        private UnityEvent onSuccess;

        public IObservable<Unit> OnSuccessAsObservable() => onSuccess.AsObservable();

        #endregion

        public void GameStart()
        {
            IsPlaying = true;
            OnGameStart();
        }

        protected virtual void OnGameStart()
        {
            GameManager.Instance.Status.StartTimeCount();
        }

        /// <summary>
        /// 成功
        /// </summary>
        public void Success()
        {
            IsSuccess = true;

            GameManager.Instance.Status.StopTimeCount();

            OnSuccess();
            onSuccess.Invoke();
        }

        public virtual void OnSuccess()
        {
        }
    }
}