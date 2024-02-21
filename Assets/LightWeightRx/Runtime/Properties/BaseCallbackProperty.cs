using System;

namespace MbsCore.LightWeightRx
{
    public abstract class BaseCallbackProperty<TValue> : IReadOnlyCallbackProperty<TValue>
    {
        private event Action<TValue> OnValueChanged;
        
        public abstract TValue Value { get; set; }
        
        public IDisposable Subscribe(Action<TValue> callback)
        {
            IDisposable disposable = SkipLastValueSubscribe(callback);
            callback?.Invoke(Value);
            return disposable;
        }

        public IDisposable SkipLastValueSubscribe(Action<TValue> callback)
        {
            OnValueChanged += callback;
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