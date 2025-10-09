using System;

namespace MbsCore.LightWeightRx
{
    public interface IReadOnlyCallbackProperty<TValue> : IObservable<TValue>
    {
        TValue Value { get; }
    }
}
