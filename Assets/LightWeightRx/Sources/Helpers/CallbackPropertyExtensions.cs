using System;
using UnityEngine.Events;

namespace MbsCore.LightWeightRx
{
    public static class CallbackPropertyExtensions
    {
        public static IDisposable Subscribe<T>(this ICallbackProperty<T> property, Action<T> callback)
        {
            var handler = new CallbackHandler<T>(callback);
            return property.Subscribe(handler);
        }
        
        public static IDisposable SubscribeUnity<T>(this ICallbackProperty<T> property, UnityAction<T> callback)
        {
            var handler = new CallbackHandler<T>(value =>
            {
                callback?.Invoke(value);
            });
            
            return property.Subscribe(handler);
        }
    }
}