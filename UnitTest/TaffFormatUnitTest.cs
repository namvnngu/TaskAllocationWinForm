using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TasksAllocation.Files;
using TasksAllocation.Utils.Validation;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class TaffFormatUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string taffFileName = "PT1 - Test1.taff";
            string taffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{taffFileName}";
            TaskAllocation taskAllocation = new TaskAllocation();
            Validations validations = new Validations();
            bool expectedValidTaffFile = true;
            string errorMessage = "the contents of a TAFF file do not conform to the TAFF format";

            // Act
            bool actualValidTaffFile = taskAllocation.ValidateFile(taffFile, validations);

            // Assert
            Assert.AreEqual(expectedValidTaffFile, actualValidTaffFile, errorMessage);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string taffFileName = "PT1 - Test2.taff";
            string taffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{taffFileName}";
            TaskAllocation taskAllocation = new TaskAllocation();
            Validations validations = new Validations();
            bool expectedValidTaffFile = true;
            string errorMessage = "the contents of a TAFF file do not conform to the TAFF format";

            // Act
            bool actualValidTaffFile = taskAllocation.ValidateFile(taffFile, validations);

            // Assert
            Assert.AreEqual(expectedValidTaffFile, actualValidTaffFile, errorMessage);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string taffFileName = "PT1 - Test4.taff";
            string taffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{taffFileName}";
            TaskAllocation taskAllocation = new TaskAllocation();
            Validations validations = new Validations();
            bool expectedValidTaffFile = false;
            string errorMessage = "the contents of a TAFF file conform to the TAFF format";

            // Act
            bool actualValidTaffFile = taskAllocation.ValidateFile(taffFile, validations);

            // Assert
            Assert.AreEqual(expectedValidTaffFile, actualValidTaffFile, errorMessage);
        }
    }
}
