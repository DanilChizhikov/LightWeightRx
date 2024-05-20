using System;

namespace MbsCore.LightWeightRx
{
    public sealed class CountableSkipObserver<T> : SkipObserver<T>
    {
        private int _remainingCount;

        public CountableSkipObserver(IObserver<T> source, int remainingCount) : base(source)
        {
            _remainingCount = remainingCount;
        }

        public override void OnNext(T value)
        {
            if (_remainingCount > 0)
            {
                _remainingCount--;
            }
            else
            {
                Source.OnNext(value);
            }
        }
    }
}