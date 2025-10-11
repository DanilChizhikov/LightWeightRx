using System;

namespace DTech.LightWeightRx.Tests.EditMode
{
    public class TestableReferenceClass
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}