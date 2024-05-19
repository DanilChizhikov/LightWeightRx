using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    [Serializable]
    public sealed class SerializedCallbackProperty<TValue> : CallbackProperty<TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private TValue _value;

        public SerializedCallbackProperty() : this(default!) { }
        
        public SerializedCallbackProperty(TValue value) : base(value)
        {
            _value = value;
        }

        protected override void ValueChanged(TValue value)
        {
            _value = value;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            GetValueRef() = _value;
        }
    }
}