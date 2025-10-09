using System;

namespace MbsCore.LightWeightRx
{
    public abstract class SkipObservable<T, TObserver> : IObservable<T>
        where TObserver : IObserver<T>
    {
        private readonly IObservable<T> _source;

        public SkipObservable(IObservable<T> source)
        {
            _source = source;
        }
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _source.Subscribe(GetObserver(observer));
        }
        
        protected abstract TObserver GetObserver(IObserver<T> observer);
    }
}