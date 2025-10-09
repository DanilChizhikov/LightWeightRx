using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    internal sealed class DefaultEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y) =>
            (x == null && y == null) || (y != null && y.Equals(x));

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}