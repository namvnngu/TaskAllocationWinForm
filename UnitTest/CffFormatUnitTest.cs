using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TasksAllocation.Files;
using TasksAllocation.Utils.Validation;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class CffFormatUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string cffFileName = "PT1 - Test1.cff";
            string cffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{cffFileName}";
            Configuration configuration = new Configuration();
            Validations validations = new Validations();
            bool expectedValidCffFile = true;
            string errorMessage = "the contents of a CFF file do not conform to the CFF format";

            // Act
            bool actualValidTaffFile = configuration.ValidateFile(cffFile, validations);

            // Assert
            Assert.AreEqual(expectedValidCffFile, actualValidTaffFile, errorMessage);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string cffFileName = "PT1 - Test2.cff";
            string cffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{cffFileName}";
            Configuration configuration = new Configuration();
            Validations validations = new Validations();
            bool expectedValidCffFile = true;
            string errorMessage = "the contents of a CFF file do not conform to the CFF format";

            // Act
            bool actualValidTaffFile = configuration.ValidateFile(cffFile, validations);

            // Assert
            Assert.AreEqual(expectedValidCffFile, actualValidTaffFile, errorMessage);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string cffFileName = "PT1 - Test4.cff";
            string cffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{cffFileName}";
            Configuration configuration = new Configuration();
            Validations validations = new Validations();
            bool expectedValidCffFile = false;
            string errorMessage = "the contents of a CFF file conform to the CFF format";

            // Act
            bool actualValidTaffFile = configuration.ValidateFile(cffFile, validations);

            // Assert
            Assert.AreEqual(expectedValidCffFile, actualValidTaffFile, errorMessage);
        }
    }
}
