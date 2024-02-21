using System;
using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    public interface IReadOnlyCallbackList<TValue> : IReadOnlyCallbackCollection<TValue>, IReadOnlyList<TValue>
    {
        IDisposable Subscribe(Action<IReadOnlyList<TValue>> callback);
        IDisposable SkipLastValueSubscribe(Action<IReadOnlyList<TValue>> callback);
        void Unsubscribe(Action<IReadOnlyList<TValue>> callback);
    }
}