using System;

namespace MbsCore.LightWeightRx
{
    internal sealed class CallbackHandler<T> : IObserver<T>
    {
        private readonly Action<T> _callback;

        public CallbackHandler(Action<T> callback)
        {
            _callback = callback;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            _callback.Invoke(value);
        }
    }
}