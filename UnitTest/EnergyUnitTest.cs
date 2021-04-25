using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TasksAllocation.Components;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class EnergyUnitTest
    {
        const double Accuracy = 0.0001;

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange.
            double c2 = 10.0;
            double c1 = -25.0;
            double c0 = 25.0;
            double frequency = 1.0;
            double expectedEPS = 10.0;

            // Act.
            ProcessorType processorType = new ProcessorType();
            processorType.C0 = c0;
            processorType.C1 = c1;
            processorType.C2 = c2;
            double actualEPS = processorType.EnergyPerSecond(frequency);

            Console.WriteLine(actualEPS);

            // Assert.
            // Assert.AreEqual(expectedEPS, energyPerSecond, "the amount of energy per second is incorrect");
            Assert.AreEqual(expectedEPS, actualEPS, Accuracy, "the amount of energy per second is incorrect");
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange.
            double c2 = 10.0;
            double c1 = -25.0;
            double c0 = 25.0;
            double frequency = 1.0;
            double runtime = 2.0;
            double expectedEnergy = 20.0;

            // Act.
            ProcessorType processorType = new ProcessorType();
            processorType.C0 = c0;
            processorType.C1 = c1;
            processorType.C2 = c2;
            double actualEnergy = processorType.CalculateEnergy(frequency, runtime);

            // Assert.
            // Assert.AreEqual(expectedEnergy, energy, "the amount of energy consumed is incorrect");
            Assert.AreEqual(expectedEnergy, actualEnergy, Accuracy, "the amount of energy consumed is incorrect");
        }
    }
}
