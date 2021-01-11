using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Unlocks.Domain;

namespace Unlocks.View
{
    public class GesturedPoint : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField]
        private int index;

        public int Index => index;

        private RectTransform rectTransform;

        public RectTransform RectTransform => rectTransform;

        private IGesturedPointHandler _handler;

        public int TouchOrder { get; private set; }

        private void Awake()
        {
            rectTransform = transform as RectTransform;
        }

        public void Initialize(IGesturedPointHandler handler)
        {
            _handler = handler;
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                transform.DOScale(1.2f, 0.2f);
            }
            else
            {
                transform.DOScale(1f, 0.2f);
            }
        }

        /// <summary>
        /// タッチされる順番
        /// </summary>
        /// <param name="order"></param>
        public void SetTouchOrder(int order)
        {
            TouchOrder = order;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("Enter => " + gameObject.name);
            _handler.OnDragEnter(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Debug.Log("Start point => " + gameObject.name);
            _handler.OnDragStart(this);
        }
    }
}