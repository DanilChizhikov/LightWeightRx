using System;

namespace MbsCore.LightWeightRx
{
    public interface IReadOnlyCallbackProperty<TValue>
    {
        TValue Value { get; }
        
        IDisposable Subscribe(Action<TValue> callback);
        IDisposable SkipLastValueSubscribe(Action<TValue> callback);
        void Unsubscribe(Action<TValue> callback);
    }
}
