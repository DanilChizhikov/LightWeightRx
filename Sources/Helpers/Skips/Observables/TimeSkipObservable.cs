using System;

namespace MbsCore.LightWeightRx
{
    public abstract class TimeSkipObservable<T> : SkipObservable<T, TimeSkipObserver<T>>
    {
        private readonly float _duration;

        public TimeSkipObservable(IObservable<T> source) : base(source)
        {
        }
    }
}