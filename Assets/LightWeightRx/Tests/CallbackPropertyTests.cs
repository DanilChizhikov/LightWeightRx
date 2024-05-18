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
        public void Subscribe_To_Int_Property_And_Set_Value_Int_Max_New_Value_Not_Equal_Last_Value()
        {
            var property = new CallbackProperty<int>(0);
            property.Value = int.MaxValue;
            Assert.AreNotEqual(property.Value, 0);
        }
    }
}