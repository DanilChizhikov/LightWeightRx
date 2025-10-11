using System;
using UnityEngine;

namespace DTech.LightWeightRx
{
    public sealed class FixedUnscaledTimeSkipObserver<T> : TimeSkipObserver<T>
    {
        public FixedUnscaledTimeSkipObserver(IObserver<T> source, float duration) : base(source, duration)
        {
        }

        protected override float GetCurrentTime()
        {
            return Time.fixedUnscaledTime;
        }
    }
}