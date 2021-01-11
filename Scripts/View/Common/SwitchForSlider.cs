using System;
using Unlocks.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Kameffee.Unlocks.View
{
    [RequireComponent(typeof(Slider))]
    public class SwitchForSlider : MonoBehaviour, ISwitch
    {
        private Slider slider;

        public IObservable<bool> OnChangedValue() => slider.OnValueChangedAsObservable().Select(f => (int) f == 1);

        public bool Value
        {
            get => slider.value == 1;
            set => slider.value = value ? 1 : 0;
        }

        public void Initialize(bool initialValue)
        {
            slider = GetComponent<Slider>();
            slider.SetValueWithoutNotify(initialValue ? 1 : 0);
        }
    }
}