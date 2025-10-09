using System;

namespace MbsCore.LightWeightRx
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> property, Action<T> callback)
        {
            var handler = new AnonymousObserver<T>(callback);
            return property.Subscribe(handler);
        }
        
        public static IObservable<T> Skip<T>(this IObservable<T> source, int count = 1)
        {
            ThrowIfInvalidCount(count, 0);
            return new CountableSkipObservable<T>(source, count);
        }
        
        private static void ThrowIfInvalidCount(int count, int minValue)
        {
            if (count < minValue)
            {
                throw new ArgumentOutOfRangeException("count");
            }
        }
    }
}