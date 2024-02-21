using System;
using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    public interface IReadOnlyCallbackCollection<TValue> : IReadOnlyCollection<TValue>
    {
        IDisposable Subscribe(Action<IReadOnlyCollection<TValue>> callback);
        IDisposable SkipLastValueSubscribe(Action<IReadOnlyCollection<TValue>> callback);
        void Unsubscribe(Action<IReadOnlyCollection<TValue>> callback);
    }
}