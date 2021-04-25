using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TasksAllocation.Components;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class DownloadUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Processor processor = new Processor();
            processor.Download = 380;
            Task task = new Task();
            task.Download = 120;
            bool expectedValue = true;
            string errorMessage = "the amount of download speed required by a task is higher than" +
                " the amount of download speed provided by a processor";

            // Act
            bool actualValue = processor.IsDownloadSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            Processor processor = new Processor();
            processor.Download = 300;
            Task task = new Task();
            task.Download = 300;
            bool expectedValue = true;
            string errorMessage = "the amount of download speed required by a task is higher than" +
                " the amount of download speed provided by a processor";

            // Act
            bool actualValue = processor.IsDownloadSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            Processor processor = new Processor();
            processor.Download = 300;
            Task task = new Task();
            task.Download = 330;
            bool expectedValue = false;
            string errorMessage = "the amount of download speed required by a task is less than or equal to" +
                " the amount of download speed provided by a processor";

            // Act
            bool actualValue = processor.IsDownloadSufficient(task);

            // Assert
            Assert.AreEqual(expectedValue, actualValue, errorMessage);
        }
    }
}
