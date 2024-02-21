using System;

namespace MbsCore.LightWeightRx
{
    internal sealed class CallbackPropertyDisposableHandler : IDisposable
    {
        private readonly Action _unsubscribe;

        public CallbackPropertyDisposableHandler(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }
        
        public void Dispose()
        {
            _unsubscribe?.Invoke();
        }
    }
}