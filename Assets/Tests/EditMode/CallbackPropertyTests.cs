using System;
using NUnit.Framework;
using Random = UnityEngine.Random;

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

        [Test]
        public void Subscribe_Three_Times_Should_Not_Throw_Exception()
        {
            var property = new CallbackProperty<int>(int.MinValue);
            int result1 = int.MinValue;
            int result2 = int.MinValue;
            int result3 = int.MinValue;
            
            property.Subscribe(value =>
            {
                result1 = value;
            });
            
            property.Subscribe(value =>
            {
                result2 = value;
            });
            
            property.Subscribe(value =>
            {
                result3 = value;
            });
            
            property.Value = 1;
            Assert.AreNotEqual(result1, int.MinValue);
            Assert.AreNotEqual(result2, int.MinValue);
            Assert.AreNotEqual(result3, int.MinValue);
        }
        
        [Test]
        public void Subscribe_Two_Times_And_Composite_Dispose_Should_Not_Change_Values()
        {
            var property = new CallbackProperty<int>(int.MinValue);
            var composite = new CompositeDisposable();
            int result1 = int.MinValue;
            int result2 = int.MinValue;

            IDisposable subscribe1 = property.Subscribe(value =>
            {
                result1 = value;
            });

            IDisposable subscribe2 = property.Subscribe(value =>
            {
                result2 = value;
            });

            composite.Add(subscribe1);
            composite.Add(subscribe2);
            composite.Dispose();
            
            property.Value = 1;
            Assert.AreEqual(result1, int.MinValue);
            Assert.AreEqual(result2, int.MinValue);
        }
        
        [Test]
        public void Subscribe_With_Skip_LastValue_And_Values_Should_Not_Be_Equals()
        {
            int result = int.MinValue;
            var property = new CallbackProperty<int>(result);
            property.Value = int.MaxValue;
            property.Skip().Subscribe(value =>
            {
                result = value;
            });
            
            Assert.AreNotEqual(result, property.Value);
        }

        [Test]
        public void Subscribe_With_Skip_LastValue_And_Update_Value_Values_Should_Be_Equals()
        {
            int result = int.MinValue;
            var property = new CallbackProperty<int>(result);
            property.Value = int.MaxValue;
            property.Skip().Subscribe(value =>
            {
                result = value;
            });

            property.Value = Random.Range(int.MinValue, int.MaxValue);
            Assert.AreEqual(result, property.Value);
        }

        [Test]
        public void Set_ReferenceType_Value_With_Default_Equality_Comparer_Should_Not_Throw_Exception_When_Set_NullRef()
        {
            var property = new CallbackProperty<TestableReferenceClass>(new TestableReferenceClass());
            property.Value = null;
        }
        
        [Test]
        public void Set_ReferenceType_Value_With_Null_Equality_Comparer_Should_Not_Throw_Exception_When_Set_NullRef()
        {
            var property = new CallbackProperty<TestableReferenceClass>(new TestableReferenceClass(), null);
            property.Value = null;
        }
    }
}