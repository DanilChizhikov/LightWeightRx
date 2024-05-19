using System;
using UnityEngine.Events;

namespace MbsCore.LightWeightRx
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> property, Action<T> callback)
        {
            var handler = new AnonymousObserver<T>(callback);
            return property.Subscribe(handler);
        }
        
        public static IDisposable SubscribeUnity<T>(this IObservable<T> property, UnityAction<T> callback)
        {
            var handler = new AnonymousObserver<T>(value =>
            {
                callback?.Invoke(value);
            });
            
            return property.Subscribe(handler);
        }
    }
}