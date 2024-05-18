using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    [Serializable]
    public sealed class SerializedCallbackProperty<TValue> : BaseCallbackProperty<TValue>
    {
        [SerializeField] private TValue _value;

        public override TValue Value
        {
            get => _value;
            set
            {
                if(!CanSetValue(value))
                {
                    return;
                }

                _value = value;
                SendCallbacks();
            }
        }

        public SerializedCallbackProperty() : this(default) { }
        
        public SerializedCallbackProperty(TValue value)
        {
            _value = value;
        }
    }
}