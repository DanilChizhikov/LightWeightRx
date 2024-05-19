using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    internal sealed class AnonymousObserver<T> : IObserver<T>
    {
        private readonly Action<T> _callback;

        public AnonymousObserver(Action<T> callback)
        {
            _callback = callback;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            Debug.LogError(error);
        }

        public void OnNext(T value)
        {
            _callback.Invoke(value);
        }
    }
}