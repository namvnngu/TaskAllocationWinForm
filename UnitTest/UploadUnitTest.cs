using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TasksAllocation.Components;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class UploadUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Processor processor = new Processor();
            processor.Upload = 50;
            Task task = new Task();
            task.Upload = 12;
            bool expectedValue = true;
            string errorMessage = "the amount of upload speed required by a task is higher than" +
                " the amount of upload speed provided by a processor";

            // Act
            bool actualValue = processor.IsUploadSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            Processor processor = new Processor();
            processor.Upload = 30;
            Task task = new Task();
            task.Upload = 30;
            bool expectedValue = true;
            string errorMessage = "the amount of upload speed required by a task is higher than" +
                " the amount of upload speed provided by a processor";

            // Act
            bool actualValue = processor.IsUploadSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            Processor processor = new Processor();
            processor.Upload = 45;
            Task task = new Task();
            task.Upload = 50;
            bool expectedValue = false;
            string errorMessage = "the amount of upload speed required by a task is less than or equal to" +
                " the amount of upload speed provided by a processor";

            // Act
            bool actualValue = processor.IsUploadSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }
    }
}
