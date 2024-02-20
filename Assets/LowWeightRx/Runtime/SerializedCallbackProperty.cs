using System;
using UnityEngine;

namespace MbsCore.LowWeightRx
{
    [Serializable]
    public sealed class SerializedCallbackProperty<TValue> : BaseCallbackProperty<TValue>
    {
        [SerializeField] private TValue _value;

        public override TValue Value
        {
            get => _value;
            set => TrySetValue(value);
        }
        
        public SerializedCallbackProperty(TValue value) : base(value)
        {
            _value = default;
        }
    }
}