using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Unlocks.View
{
    public class MeterDot : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        
        public bool IsVisible { get; private set; }

        private void Awake()
        {
            image.DOFade(0, 0);
        }

        public void SetVisible(bool visible)
        {
            if (IsVisible != visible)
            {
                if (visible)
                {
                    image.DOFade(1, 0.5f)
                        .SetEase(Ease.Flash, 7);
                }
                else
                {
                    image.DOFade(0, 0.5f);
                }
            }
            
            IsVisible = visible;
        }
    }
}