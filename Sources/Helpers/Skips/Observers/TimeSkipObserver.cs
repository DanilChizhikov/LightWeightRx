using System;

namespace DTech.LightWeightRx
{
    public abstract class TimeSkipObserver<T> : SkipObserver<T>
    {
        private readonly float _endWaitTime;

        public TimeSkipObserver(IObserver<T> source, float duration) : base(source)
        {
            _endWaitTime = GetCurrentTime() + duration;
        }

        public sealed override void OnNext(T value)
        {
            if(_endWaitTime < GetCurrentTime())
            {
                Source.OnNext(value);
            }
        }

        protected abstract float GetCurrentTime();
    }
}