using System;

namespace MbsCore.LightWeightRx
{
    public interface ICallbackProperty<TValue> : IReadOnlyCallbackProperty<TValue>, IObservable<TValue>
    {
    }
}