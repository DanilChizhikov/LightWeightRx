using System;

namespace MbsCore.LightWeightRx.Tests
{
    public class TestableReferenceClass
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}