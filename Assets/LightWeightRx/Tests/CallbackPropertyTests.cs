using NUnit.Framework;

namespace MbsCore.LightWeightRx.Tests
{
    [TestFixture]
    public sealed class CallbackPropertyTests
    {
        [Test]
        public void Create_Int32_Property_Should_Not_Be_Null()
        {
            var property = new CallbackProperty<int>(0);
            Assert.IsNotNull(property);
        }

        [Test]
        public void Set_Int_Max_Value_To_Property_After_Construct_And_Value_Should_Not_Be_Zero()
        {
            var property = new CallbackProperty<int>(0);
            property.Value = int.MaxValue;
            Assert.AreNotEqual(property.Value, 0);
        }
    }
}