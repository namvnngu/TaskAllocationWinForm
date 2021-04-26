using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using TasksAllocation.Files;
using TasksAllocation.Utils.Validation;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class CffErrorDetectionUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string cffFileName = "PT1 - Test4.cff";
            string cffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{cffFileName}";
            Configuration configuration = new Configuration();
            Validations validations = new Validations();
            int expectedErrorCode = 10;
            string expectedErrorMessage = "TOTAL-RAM=987 is invalid";
            string expectedErrorLinenumber = "175";

            // Act
            configuration.ValidateFile(cffFile, validations);
            ErrorManager errorManager = validations.ErrorValidationManager;
            List<Error> errors = errorManager.Errors;
            Error actualError = new Error();

            foreach (Error error in errors)
            {
                if (error.ErrorCode == expectedErrorCode &&
                    error.Message == expectedErrorMessage &&
                    error.LineNumber == expectedErrorLinenumber)
                {
                    actualError = error;
                    break;
                }
            }

            // Assert
            Assert.AreEqual(expectedErrorCode, actualError.ErrorCode, "The error code is incorrect");
            Assert.AreEqual(expectedErrorMessage, actualError.Message, "The error message is incorrect");
            Assert.AreEqual(expectedErrorLinenumber, actualError.LineNumber, "The error's line number is incorrect");
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string cffFileName = "PT1 - Test4.cff";
            string cffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{cffFileName}";
            Configuration configuration = new Configuration();
            Validations validations = new Validations();
            int expectedErrorCode = 10;
            string expectedErrorMessage = "Gbps=123 is invalid";
            string expectedErrorLinenumber = "176";

            // Act
            configuration.ValidateFile(cffFile, validations);
            ErrorManager errorManager = validations.ErrorValidationManager;
            List<Error> errors = errorManager.Errors;
            Error actualError = new Error();

            foreach (Error error in errors)
            {
                if (error.ErrorCode == expectedErrorCode &&
                    error.Message == expectedErrorMessage &&
                    error.LineNumber == expectedErrorLinenumber)
                {
                    actualError = error;
                    break;
                }
            }

            // Assert
            Assert.AreEqual(expectedErrorCode, actualError.ErrorCode, "The error code is incorrect");
            Assert.AreEqual(expectedErrorMessage, actualError.Message, "The error message is incorrect");
            Assert.AreEqual(expectedErrorLinenumber, actualError.LineNumber, "The error's line number is incorrect");
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
            int expectedErrorCode = 13;
            string expectedErrorMessage = "Invalid key-value (integer value) format";
            string expectedErrorLinenumber = "96";

            // Act
            configuration.ValidateFile(cffFile, validations);
            ErrorManager errorManager = validations.ErrorValidationManager;
            List<Error> errors = errorManager.Errors;
            Error actualError = new Error();

            foreach (Error error in errors)
            {
                if (error.ErrorCode == expectedErrorCode &&
                    error.Message == expectedErrorMessage &&
                    error.LineNumber == expectedErrorLinenumber)
                {
                    actualError = error;
                    break;
                }
            }

            // Assert
            Assert.AreEqual(expectedErrorCode, actualError.ErrorCode, "The error code is incorrect");
            Assert.AreEqual(expectedErrorMessage, actualError.Message, "The error message is incorrect");
            Assert.AreEqual(expectedErrorLinenumber, actualError.LineNumber, "The error's line number is incorrect");
        }
    }
}
