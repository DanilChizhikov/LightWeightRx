using System;

namespace MbsCore.LightWeightRx
{
    internal sealed class CallbackHandler<T> : IObserver<T>
    {
        private readonly Action<T> _unsubscribe;

        public CallbackHandler(Action<T> unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            _unsubscribe.Invoke(value);
        }
    }
}