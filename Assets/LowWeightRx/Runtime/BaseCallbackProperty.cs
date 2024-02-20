using System;

namespace MbsCore.LowWeightRx
{
    public abstract class BaseCallbackProperty<TValue> : IReadOnlyCallbackProperty<TValue>
    {
        private event Action<TValue> OnValueChanged;
        
        public abstract TValue Value { get; set; }
        
        public BaseCallbackProperty(TValue value)
        {
            Value = value;
        }
        
        public IDisposable Subscribe(Action<TValue> callback, bool skipLastValue = true)
        {
            OnValueChanged += callback;
            if (!skipLastValue)
            {
                callback?.Invoke(Value);
            }

            return new CallbackPropertyDisposableHandler(() => Unsubscribe(callback));
        }

        public void Unsubscribe(Action<TValue> callback)
        {
            OnValueChanged -= callback;
        }
        
        protected void TrySetValue(TValue value)
        {
            if ((value == null && Value == null) ||
                (value != null && value.Equals(Value)))
            {
                return;
            }
            
            Value = value;
            RiseCallbacks();
        }
        
        private void RiseCallbacks()
        {
            OnValueChanged?.Invoke(Value);
        }
    }
}