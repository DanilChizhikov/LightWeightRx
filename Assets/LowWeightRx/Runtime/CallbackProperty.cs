namespace MbsCore.LowWeightRx
{
    public sealed class CallbackProperty<TValue> : BaseCallbackProperty<TValue>
    {
        private TValue _value;

        public override TValue Value
        {
            get => _value;
            set => TrySetValue(value);
        }
        
        public CallbackProperty(TValue value) : base(value)
        {
            _value = default;
        }
    }
}