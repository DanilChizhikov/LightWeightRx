using System;
using UnityEngine.Events;

namespace MbsCore.LightWeightRx
{
    public static class CallbackPropertyExtensions
    {
        public static IDisposable SubscribeSkipLastValue<T>(this ICallbackProperty<T> property, Action<T> callback)
        {
            var handler = new AnonymousObserver<T>(callback);
            return property.SubscribeSkipLastValue(handler);
        }
        
        public static IDisposable SubscribeSkipLastValueUnity<T>(this ICallbackProperty<T> property, UnityAction<T> callback)
        {
            var handler = new AnonymousObserver<T>(value =>
            {
                callback?.Invoke(value);
            });
            
            return property.SubscribeSkipLastValue(handler);
        }
    }
}