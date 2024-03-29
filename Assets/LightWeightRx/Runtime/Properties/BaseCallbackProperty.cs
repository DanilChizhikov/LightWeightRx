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
            return new CallbackDisposableHandler(() => Unsubscribe(callback));
        }

        public void Unsubscribe(Action<TValue> callback)
        {
            OnValueChanged -= callback;
        }
        
        protected bool CanSetValue(TValue value)
        {
            if ((value == null && Value == null) ||
                (value != null && value.Equals(Value)))
            {
                return false;
            }

            return true;
        }
        
        protected void SendCallbacks()
        {
            OnValueChanged?.Invoke(Value);
        }
    }
}