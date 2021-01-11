using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Unlocks.View
{
    public class CardScanner : MonoBehaviour
    {
        [SerializeField]
        private float threshold = 0.15f;

        public float Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        private Subject<bool> _onResult = new Subject<bool>();
        public IObservable<bool> OnResult() => _onResult;

        private bool IsAutoMove { get; set; }

        private Transform target;

        private Vector2 velocity;
        private Vector3 lastPosition;

        public async void Failed()
        {
            _onResult.OnNext(false);

            IsAutoMove = true;
            await target.DOMove(new Vector3(-0.77f, 2.5f, 0), 0.5f);
            IsAutoMove = false;
            lastPosition = target.position;
        }

        private async void OnTriggerEnter2D(Collider2D other)
        {
            if (IsAutoMove) return;

            target = other.transform;
            other.GetComponent<DragAndDrop>().IsFixedHorizontal = true;
            await other.transform.DOMove(new Vector3(-0.77f, 2.5f, 0), 0.5f);
            lastPosition = other.transform.position;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (IsAutoMove) return;

            var vector = other.transform.position - lastPosition;
            Debug.Log(vector);
            // 失敗判定
            if (Math.Abs(vector.y - (-0.1f)) < threshold)
            {
            }
            else if (Math.Abs(vector.y) > 0.01f)
            {
                Debug.Log("Failed");
                Failed();
            }

            lastPosition = other.transform.position;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsAutoMove) return;

            Debug.Log("Success");
            _onResult.OnNext(true);
        }
    }
}