using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    public sealed class FixedTimeSkipObserver<T> : TimeSkipObserver<T>
    {
        public FixedTimeSkipObserver(IObserver<T> source, float duration) : base(source, duration)
        {
        }

        protected override float GetCurrentTime()
        {
            return Time.fixedTime;
        }
    }
}