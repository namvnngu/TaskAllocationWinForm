using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TasksAllocation.Components;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class RamUnitTest
    {
        const double Accuracy = 0.0001;

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Processor processor = new Processor();
            processor.RAM = 4;
            Task task = new Task();
            task.RAM = 2;
            bool expectedValue = true;
            string errorMessage = "The amount of RAM required by a task is higher than" +
                " the amount of RAM associated with a processor";

            // Act
            bool actualValue = processor.IsRamSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }

        [TestMethod]
        public void TestMethod2()
        {
             // Arrange
            Processor processor = new Processor();
            processor.RAM = 2;
            Task task = new Task();
            task.RAM = 2;
            bool expectedValue = true;
            string errorMessage = "The amount of RAM required by a task is higher than" +
                " the amount of RAM associated with a processor";

            // Act
            bool actualValue = processor.IsRamSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            Processor processor = new Processor();
            processor.RAM = 2;
            Task task = new Task();
            task.RAM = 4;
            bool expectedValue = false;
            string errorMessage = "The amount of RAM required by a task is higher than" +
                " the amount of RAM associated with a processor";

            // Act
            bool actualValue = processor.IsRamSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }
    }
}
