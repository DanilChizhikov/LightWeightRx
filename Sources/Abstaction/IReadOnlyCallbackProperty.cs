using System;

namespace DTech.LightWeightRx
{
    public interface IReadOnlyCallbackProperty<TValue> : IObservable<TValue>
    {
        TValue Value { get; }
    }
}
