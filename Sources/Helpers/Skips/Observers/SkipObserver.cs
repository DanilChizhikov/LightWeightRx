using System;

namespace MbsCore.LightWeightRx
{
    public abstract class SkipObserver<T> : IObserver<T>, IDisposable
    {
        protected IObserver<T> Source { get; }

        public SkipObserver(IObserver<T> source)
        {
            Source = source;
        }
        
        public void OnCompleted()
        {
            Source.OnCompleted();
        }

        public void OnError(Exception error)
        {
            Source.OnError(error);
        }

        public virtual void OnNext(T value)
        {
            Source.OnNext(value);
        }

        public virtual void Dispose()
        {
        }
    }
}