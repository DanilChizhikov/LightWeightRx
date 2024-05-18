using System;
using UnityEngine;

namespace MbsCore.LightWeightRx
{
    [Serializable]
    public sealed class SerializedCallbackProperty<TValue> : ICallbackProperty<TValue>
    {
        private readonly ICallbackProperty<TValue> _internalProperty;
        
        [SerializeField] private TValue _value;

        public TValue Value => _internalProperty.Value;

        public SerializedCallbackProperty() : this(default) { }
        
        public SerializedCallbackProperty(TValue value)
        {
            _value = value;
            _internalProperty = new CallbackProperty<TValue>(_value);
        }
        
        public IDisposable Subscribe(IObserver<TValue> observer)
        {
            lock (this)
            {
                return _internalProperty.Subscribe(observer);
            }
        }
    }
}