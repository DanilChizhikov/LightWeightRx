using System;

namespace MbsCore.LowWeightRx
{
    internal sealed class CallbackPropertyDisposableHandler<T> : IDisposable
    {
        private readonly IReadOnlyCallbackProperty<T> _property;
        private readonly Action<T> _callback;

        public CallbackPropertyDisposableHandler(IReadOnlyCallbackProperty<T> property, Action<T> callback)
        {
            _property = property;
            _callback = callback;
        }
        
        public void Dispose()
        {
            _property.RemoveListener(_callback);
        }
    }
}