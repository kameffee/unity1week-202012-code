using UnityEngine;
using UnityEngine.EventSystems;

namespace Unlocks.View.Common
{
    public class ButtonText : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private bool isActive = true;

        [SerializeField]
        private RectTransform text;

        [SerializeField]
        private Vector2 onPushedPosition;

        private Vector2 defaultPosition;

        private void Awake()
        {
            defaultPosition = text.anchoredPosition;
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isActive) return;
            text.anchoredPosition = onPushedPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isActive) return;
            text.anchoredPosition = defaultPosition;
        }
    }
}