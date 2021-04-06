using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;
using System.IO;

namespace TasksAllocation.Utils.Validation
{
    class Validations
    {
        public static bool CheckExtension(
            string filePath, 
            string expectedExtension, 
            ref ErrorManager errorManager,
            string fileName, 
            string lineNumber)
        {
            string extractedExtension = Path.GetExtension(filePath);
            Error error = new Error();

            if (extractedExtension == "")
            {

                error.Message = "File extension cannot be found";
                error.ActualValue = "null";
                error.ExpectedValue = $".{expectedExtension}";
                error.Filename = fileName;
                error.LineNumber = lineNumber;
                error.ErrorCode = ErrorCode.INVALID_EXTENSION;

                errorManager.Errors.Add(error);

                return false;
            }

            if (extractedExtension == $".{expectedExtension}")
            {
                return true;
            }

            error.Message = "The file extension is invalid";
            error.ActualValue = extractedExtension;
            error.ExpectedValue = $".{expectedExtension}";
            error.Filename = fileName;
            error.LineNumber = lineNumber;
            error.ErrorCode = ErrorCode.INVALID_EXTENSION;

            errorManager.Errors.Add(error);

            return false;
        }

        public static bool CheckProcessedFileExists(
            string filename, 
            string expectedFilename,
            ref ErrorManager errorManager, 
            string fileName)
        {
            if (filename == null)
            {
                string message = $"There is no expected file";
                string actualValue = "null";
                string expectedValue = expectedFilename;
                Error error = new Error(
                    message, 
                    actualValue, 
                    expectedValue, 
                    fileName,
                    "", 
                    ErrorCode.MISSING_FILE);

                errorManager.Errors.Add(error);

                return false;
            }

            return true;
        }

        public static string[] CheckPairKeyValue(
            string line, 
            string key, 
            string value, 
            ref ErrorManager errorManager, 
            string fileName, 
            string lineNumber)
        {
            string[] lineData = line.Split(Symbols.EQUALITY);
            const int N_LINE_DATA = 2;

            if (lineData.Length == N_LINE_DATA)
            {
                return lineData;
            }

            string message = $"No value for {key} can be found";
            string actualValue = "null";
            string expectedValue = $"{key}={value}";
            Error error = new Error(
                message, 
                actualValue, 
                expectedValue, 
                fileName, 
                lineNumber,
                ErrorCode.MISSING_VALUE);

            errorManager.Errors.Add(error);

            return null;
        }

        public static string CheckTextValueExist(
            string text, 
            string actualValue, 
            string expectedValue, 
            ref ErrorManager errorManager, 
            string fileName, 
            string lineNumber)
        {
            string value = text.Trim(Symbols.DOUBLE_QUOTE);

            if (value.Length > 0)
            {
                return value;
            }

            string message = "No valid file/text/value can be found";
            Error error = new Error(
                message, 
                actualValue, 
                expectedValue, 
                fileName, 
                lineNumber, 
                ErrorCode.MISSING_VALUE);

            errorManager.Errors.Add(error);

            return null;
        }

        public static bool CheckInvalidFileNameChars(
            string filePath, 
            ref ErrorManager errorManager, 
            string fileName, 
            string lineNumber)
        {
            if (filePath.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
            {
                return true;
            }

            string message = "File path contains invalid characters";
            string actualValue = filePath;
            string expectedValue = "Path must not contain <, >, :, \", /, \\, |, ?, *";
            Error error = new Error(
                message, 
                actualValue, 
                expectedValue, 
                fileName, 
                lineNumber, 
                ErrorCode.INVALID_FORMAT);

            errorManager.Errors.Add(error);

            return false;
        }

        public static bool CheckFileExists(
            string filePath, 
            ref ErrorManager errorManager, 
            string fileName, string 
            lineNumber)
        {
            if (File.Exists(filePath))
            {
                return true;
            }

            string message = "File does not exist";
            string actualValue = filePath;
            string expectedValue = "Please provide valid file path";
            Error error = new Error(
                message, 
                actualValue,
                expectedValue, 
                fileName, 
                lineNumber, 
                ErrorCode.NOT_FOUND);

            errorManager.Errors.Add(error);

            return false;
        }

        public static int CheckInteger(
            string number, 
            string keyword,
            ref ErrorManager errorManager,
            string fileName, 
            string lineNumber)
        {
            int parsedInt = -1;
            if(int.TryParse(number, out parsedInt))
            {
                return parsedInt;
            }

            string message = "Cannot be parsed to an integer";
            string actualValue = number;
            string expectedValue = $"A interger for \"{keyword}\"";
            Error error = new Error(
                message, 
                actualValue, 
                expectedValue, 
                fileName, 
                lineNumber, 
                ErrorCode.INVALID_VALUE);

            errorManager.Errors.Add(error);

            return parsedInt;
        }

        public static int ValidateIntegerPair(
            string line, 
            string keyword,
            ref ErrorManager errorManager,
            string fileName,
            string lineNumber)
        {
            string count = null;
            int returnedCount = -1;
            // Check whether the pair of key-value exists
            string[] lineCountData = CheckPairKeyValue(
                line,
                keyword,
                "(an integer)",
                ref errorManager,
                fileName,
                lineNumber.ToString());

            if (lineCountData != null)
            {
                count = CheckTextValueExist(
                    lineCountData[1],
                    "null",
                    "A string has one or more than one characters",
                    ref errorManager,
                    fileName,
                    lineNumber.ToString());
            }

            // Check whether the value is an integer
            if (count != null)
            {
                returnedCount = CheckInteger(
                    count,
                    keyword,
                    ref errorManager,
                    fileName, 
                    lineNumber.ToString());
            }

            return returnedCount;
        }
    }
}
