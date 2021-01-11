using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Unlocks.View
{
    public class TouchCardArea : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onScan;

        public IObservable<Unit> OnScan() => onScan.AsObservable();

        private void OnTriggerEnter2D(Collider2D other)
        {
            onScan.Invoke();
        }
    }
}