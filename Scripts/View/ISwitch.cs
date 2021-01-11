using System;

namespace Unlocks.View
{
    public interface ISwitch
    {
        void Initialize(bool initialValue);

        bool Value { get; set; }

        IObservable<bool> OnChangedValue();
    }
}