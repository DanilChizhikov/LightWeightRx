using System;

namespace MbsCore.LowWeightRx
{
    public interface IReadOnlyCallbackProperty<TValue>
    {
        TValue Value { get; }
        
        IDisposable AddListener(Action<TValue> callback, bool isRise = false);
        void RemoveListener(Action<TValue> callback);
    }
}
