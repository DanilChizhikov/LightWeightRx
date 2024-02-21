namespace MbsCore.LightWeightRx
{
    public sealed class CallbackProperty<TValue> : BaseCallbackProperty<TValue>
    {
        private TValue _value;

        public override TValue Value
        {
            get => _value;
            set => TrySetValue(value);
        }

        public CallbackProperty() : this(default) { }
        
        public CallbackProperty(TValue value)
        {
            _value = value;
        }
    }
}