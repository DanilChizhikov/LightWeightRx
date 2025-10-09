using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    public sealed class DefaultUnscaledTimeSkipObserver<T> : TimeSkipObserver<T>
    {
        public DefaultUnscaledTimeSkipObserver(IObserver<T> source, float duration) : base(source, duration)
        {
        }

        protected override float GetCurrentTime()
        {
            return Time.unscaledTime;
        }
    }
}