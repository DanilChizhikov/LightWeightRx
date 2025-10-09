using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    public sealed class DefaultTimeSkipObserver<T> : TimeSkipObserver<T>
    {
        public DefaultTimeSkipObserver(IObserver<T> source, float duration) : base(source, duration)
        {
        }

        protected override float GetCurrentTime()
        {
            return Time.time;
        }
    }
}