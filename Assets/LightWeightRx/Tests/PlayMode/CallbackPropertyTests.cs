using NUnit.Framework;
using UnityEngine;

namespace MbsCore.LightWeightRx.Tests
{
    [TestFixture]
    public sealed class CallbackPropertyTests
    {
        [Test]
        public void Set_Int_Max_Value_To_SerializedProperty_After_Instantiate_And_Value_Should_Not_Be_Equal_Default()
        {
            var property = new GameObject("Test GameObject").AddComponent<SerializedCallbackPropertyTest>();
            int propertyValue = property.Property.Value;
            
            property.SetValue(int.MaxValue);
            
            Assert.AreNotEqual(propertyValue, property.Property.Value);
        }
    }
}