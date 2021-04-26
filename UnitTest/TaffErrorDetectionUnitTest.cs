using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using TasksAllocation.Files;
using TasksAllocation.Utils.Validation;

namespace TaskAllocationUnitTest
{
    [TestClass]
    public class TaffErrorDetectionUnitTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string taffFileName = "PT1 - Test4.taff";
            string taffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{taffFileName}";
            TaskAllocation taskAllocation = new TaskAllocation();
            Validations validations = new Validations();
            int expectedErrorCode = 11;
            string expectedErrorMessage = "Cannot be parsed to an integer";
            string expectedErrorLinenumber = "62";

            // Act
            taskAllocation.ValidateFile(taffFile, validations);
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
            string taffFileName = "PT1 - Test4.taff";
            string taffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{taffFileName}";
            TaskAllocation taskAllocation = new TaskAllocation();
            Validations validations = new Validations();
            int expectedErrorCode = 30;
            string expectedErrorMessage = "No valid file/text/value can be found";
            string expectedErrorLinenumber = "67";

            // Act
            taskAllocation.ValidateFile(taffFile, validations);
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
            string taffFileName = "PT1 - Test4.taff";
            string taffFile = $"{rootPath}TestFiles{Path.DirectorySeparatorChar}{taffFileName}";
            TaskAllocation taskAllocation = new TaskAllocation();
            Validations validations = new Validations();
            int expectedErrorCode = 10;
            string expectedErrorMessage = "LOCATIONS is invalid";
            string expectedErrorLinenumber = "15";

            // Act
            taskAllocation.ValidateFile(taffFile, validations);
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
