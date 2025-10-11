using System;
using UnityEngine;

namespace DTech.LightWeightRx
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