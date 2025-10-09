namespace MbsCore.LightWeightRx
{
    public interface ICallbackProperty<TValue> : IReadOnlyCallbackProperty<TValue>
    {
        new TValue Value { get; set; }
    }
}