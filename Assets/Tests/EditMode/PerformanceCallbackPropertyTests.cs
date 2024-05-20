using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using Random = UnityEngine.Random;

namespace MbsCore.LightWeightRx.Tests
{
    [TestFixture]
    public sealed class PerformanceCallbackPropertyTests
    {
        [Test, Performance]
        public void Subscribe_One_Thousand_Times_Performance_Test()
        {
            using var scope = Measure.Scope();
            var property = new CallbackProperty<int>(0);
            Measure.Method(() =>
                {
                    CreateSubscribe(property, 1);
                }).WarmupCount(10)
                .SampleGroup($"Subscribe Group")
                .MeasurementCount(999)
                .Run();
        }

        [Test, Performance]
        public void Set_One_Thousand_Times_Value_Performance_Test()
        {
            var property = new CallbackProperty<int>(0);
            CreateSubscribe(property, 1000);
            using var scope = Measure.Scope();
            Measure.Method(() =>
                {
                    property.Value = Random.Range(0, int.MaxValue);
                }).WarmupCount(10)
                .MeasurementCount(999)
                .SampleGroup($"Set Group")
                .Run();
        }

        [Test, Performance]
        public void Test_List_One_Thousand_Times_Value_Performance_Test()
        {
            var list = new List<Action<int>>();
            for (int i = 0; i < 1000; i++)
            {
                int inclidedValue = int.MinValue;
                list.Add(value => inclidedValue = value);
            }
            
            using var scope = Measure.Scope();
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        list[i](Random.Range(0, int.MaxValue));
                    }
                }).WarmupCount(10)
                .MeasurementCount(999)
                .Run();
        }

        private void CreateSubscribe(CallbackProperty<int> property, int count)
        {
            for (int i = 0; i < count; i++)
            {
                int includedValue = int.MinValue;
                property.Subscribe(value => includedValue = value);
            }
        }
    }
}