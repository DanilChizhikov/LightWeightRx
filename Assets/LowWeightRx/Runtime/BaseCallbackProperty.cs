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
        
        public IDisposable AddListener(Action<TValue> callback, bool isRise = false)
        {
            OnValueChanged += callback;
            if (isRise)
            {
                callback?.Invoke(Value);
            }

            return new CallbackPropertyDisposableHandler<TValue>(this, callback);
        }

        public void RemoveListener(Action<TValue> callback)
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