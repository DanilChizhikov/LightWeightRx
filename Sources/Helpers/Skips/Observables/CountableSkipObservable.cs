using System;

namespace MbsCore.LightWeightRx
{
    public sealed class CountableSkipObservable<T> : SkipObservable<T, CountableSkipObserver<T>>
    {
        private readonly int _count;
        
        public CountableSkipObservable(IObservable<T> source, int count) : base(source)
        {
            _count = count;
        }

        protected override CountableSkipObserver<T> GetObserver(IObserver<T> observer)
        {
            return new CountableSkipObserver<T>(observer, _count);
        }
    }
}