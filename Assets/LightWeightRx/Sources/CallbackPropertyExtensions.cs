using System;

namespace MbsCore.LightWeightRx
{
    public static class CallbackPropertyExtensions
    {
        public static IDisposable Subscribe<T>(this ICallbackProperty<T> property, Action<T> callback)
        {
            var handler = new CallbackHandler<T>(callback);
            return property.Subscribe(handler);
        }
    }
}