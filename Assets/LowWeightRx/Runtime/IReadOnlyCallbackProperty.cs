using System;

namespace MbsCore.LowWeightRx
{
    public interface IReadOnlyCallbackProperty<TValue>
    {
        TValue Value { get; }
        
        IDisposable Subscribe(Action<TValue> callback, bool skipLastValue = true);
        void Unsubscribe(Action<TValue> callback);
    }
}
