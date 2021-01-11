using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unlocks.View
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private bool isFixedHorizontal;

        public bool IsFixedHorizontal
        {
            get => isFixedHorizontal;
            set => isFixedHorizontal = value;
        }
        
        [SerializeField]
        private bool isFixedVertical;

        public bool IsFixedVertical
        {
            get => isFixedVertical;
            set => isFixedVertical = value;
        }

        private Camera _camera;

        private Vector2 _offset;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 掴んだ位置とオブジェクトの座標との距離をキャッシュ
            _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // ドラッグ中は位置を更新する
            SetPosition(_camera.ScreenToWorldPoint(eventData.position));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _offset = Vector2.zero;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
        }

        public void SetPosition(Vector3 position)
        {
            var tf = transform;
            var toPos = new Vector3(position.x + _offset.x, position.y + _offset.y, tf.position.z);
            if (isFixedHorizontal)
            {
                toPos.x = transform.position.x;
            }
            
            if (isFixedVertical)
            {
                toPos.x = transform.position.y;
            }
            tf.position = toPos;
        }
    }
}