using System;

namespace MbsCore.LightWeightRx
{
    internal sealed class CallbackDisposableHandler : IDisposable
    {
        private readonly Action _unsubscribe;

        public CallbackDisposableHandler(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }
        
        public void Dispose()
        {
            _unsubscribe?.Invoke();
        }
    }
}