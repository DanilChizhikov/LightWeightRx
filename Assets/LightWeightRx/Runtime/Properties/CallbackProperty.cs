namespace MbsCore.LightWeightRx
{
    public sealed class CallbackProperty<TValue> : BaseCallbackProperty<TValue>
    {
        private TValue _value;

        public override TValue Value
        {
            get => _value;
            set
            {
                if (!CanSetValue(value))
                {
                    return;
                }

                _value = value;
                SendCallbacks();
            }
        }

        public CallbackProperty() : this(default) { }
        
        public CallbackProperty(TValue value)
        {
            _value = value;
        }
    }
}