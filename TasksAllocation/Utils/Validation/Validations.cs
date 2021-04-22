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
        public ErrorManager ErrorValidationManager { get; set; }
        public string Filename { get; set; }
        public string LineNumber { get; set; }

        public Validations()
        {
            ErrorValidationManager = new ErrorManager();
            Filename = null;
            LineNumber = null;
        }

        public bool CheckExtension(string filePath, string expectedExtension)
        {
            string extractedExtension = Path.GetExtension(filePath);
            string filename = Path.GetFileName(filePath);
            bool validExtensionFormat = RegexValidation.RegexExtension(filename, this);
            Error error = new Error();

            if (!validExtensionFormat && extractedExtension == "")
            {

                error.Message = "File extension cannot be found";
                error.ActualValue = "null";
                error.ExpectedValue = $".{expectedExtension}";
                error.Filename = Filename;
                error.LineNumber = LineNumber;
                error.ErrorCode = ErrorCode.INVALID_EXTENSION;

                ErrorValidationManager.Errors.Add(error);

                return false;
            }

            if (extractedExtension == $".{expectedExtension}")
            {
                return true;
            }

            error.Message = "The file extension is invalid";
            error.ActualValue = extractedExtension;
            error.ExpectedValue = $".{expectedExtension}";
            error.Filename = Filename;
            error.LineNumber = LineNumber;
            error.ErrorCode = ErrorCode.INVALID_EXTENSION;

            ErrorValidationManager.Errors.Add(error);

            return false;
        }

        public bool CheckProcessedFileExists(string filename, string expectedFilename)
        {
            if (filename == null)
            {
                string message = $"There is no expected file";
                string actualValue = "null";
                string expectedValue = expectedFilename;
                string noLinenumber = "";

                Error error = new Error(
                    message,
                    actualValue,
                    expectedValue,
                    Filename,
                    noLinenumber,
                    ErrorCode.MISSING_FILE);

                ErrorValidationManager.Errors.Add(error);

                return false;
            }

            return true;
        }

        public string[] CheckPairKeyValue(string line, string key, string value)
        {
            string[] lineData = line.Split(Symbols.EQUALITY);
            const int N_LINE_DATA = 2;

            // Check valid pair 
            RegexValidation.RegexPair(line, this);

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
                Filename,
                LineNumber,
                ErrorCode.MISSING_VALUE);

            ErrorValidationManager.Errors.Add(error);

            return null;
        }

        public string CheckTextValueExist(string text, string actualValue, string expectedValue)
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
                Filename,
                LineNumber,
                ErrorCode.MISSING_VALUE);

            ErrorValidationManager.Errors.Add(error);

            return null;
        }

        public bool CheckRequiredValueExist(string value, string keyword)
        {
            if (value == null || value == "-1")
            {
                string message = $"No valid {keyword} can be found";
                Error error = new Error(
                    message,
                    value,
                    "A value should be different from null or -1",
                    Filename,
                    LineNumber,
                    ErrorCode.MISSING_VALUE);

                ErrorValidationManager.Errors.Add(error);

                return false;
            }

            return true;
        }

        public bool CheckInvalidFileNameChars(string filePath)
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
                Filename,
                LineNumber,
                ErrorCode.INVALID_FORMAT);

            ErrorValidationManager.Errors.Add(error);

            return false;
        }

        public bool CheckFileExists(string filePath)
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
                Filename,
                LineNumber,
                ErrorCode.NOT_FOUND);

            ErrorValidationManager.Errors.Add(error);

            return false;
        }

        public bool CheckValidQuantity(string actualValue, string expectedValue, string field, int errorCode)
        {
            if (actualValue != expectedValue)
            {
                string message = $"The number of {field} is not equal to the predefined value";
                Error error = new Error(
                    message,
                    actualValue,
                    expectedValue,
                    Filename,
                    LineNumber,
                    errorCode);

                ErrorValidationManager.Errors.Add(error);

                return false;
            }

            return true;
        }

        public void CheckInvalidMap(string actualValue, string expectedValue, string errorName)
        {
            string message = $"The number of {errorName} is invalid";
            Error error = new Error(
                message,
                actualValue,
                expectedValue,
                Filename,
                LineNumber,
                ErrorCode.INVALID_MAP);

            ErrorValidationManager.Errors.Add(error);
        }

        public int CheckInteger(string number, string keyword)
        {
            int parsedInt = -1;
            if (int.TryParse(number, out parsedInt))
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
                Filename,
                LineNumber,
                ErrorCode.INVALID_VALUE);

            ErrorValidationManager.Errors.Add(error);

            return parsedInt;
        }

        public double CheckDouble(string number, string keyword)
        {
            double parsedDouble = -1;

            if (double.TryParse(number, out parsedDouble))
            {
                return parsedDouble;
            }

            string message = "Cannot be parsed to a floating number";
            string actualValue = number;
            string expectedValue = $"A floating number for \"{keyword}\"";
            Error error = new Error(
                message,
                actualValue,
                expectedValue,
                Filename,
                LineNumber,
                ErrorCode.INVALID_VALUE);

            ErrorValidationManager.Errors.Add(error);

            return parsedDouble;
        }

        public int ValidateIntegerPair(string line, string keyword)
        {
            string integer = null;
            int returnedInteger = -1;
            // Check whether the pair of key-value exists
            string[] lineCountData = CheckPairKeyValue(line, keyword, "(an integer)");

            // Check the line follows valid format
            RegexValidation.RegexIntegerPair(line, this);

            if (lineCountData != null)
            {
                integer = CheckTextValueExist(
                    lineCountData[1],
                    "null",
                    "An integer");
            }

            // Check whether the value is an integer
            if (integer != null)
            {
                returnedInteger = CheckInteger(integer, keyword);
            }

            return returnedInteger;
        }

        public double ValidateDoublePair(string line, string keyword)
        {
            string doubleNumber = null;
            double returnedDouble = -1;
            // Check whether the pair of key-value exists
            string[] lineCountData = CheckPairKeyValue(line, keyword, "(a floating number)");

            // Check the line follows valid format
            RegexValidation.RegexDoublePair(line, this);

            if (lineCountData != null)
            {
                doubleNumber = CheckTextValueExist(
                    lineCountData[1],
                    "null",
                    "A floating number");
            }

            // Check whether the value is an integer
            if (doubleNumber != null)
            {
                returnedDouble = CheckDouble(doubleNumber, keyword);
            }

            return returnedDouble;
        }

        public string ValidateStringPair(string line, string keyword)
        {
            // Check whether the pair of key-value exists 
            string[] lineData = CheckPairKeyValue(line, keyword, "\"(a string)\"");
            string data = null;

            if (lineData != null)
            {
                // Check whether the value in the above pair is valid
                data = CheckTextValueExist(
                    lineData[1],
                    "null",
                    "A string has one or more than one characters");
            }

            return data;
        }

        public bool CheckValidKeyword(string keyword, Dictionary<string, string> keywordDict)
        {
            if (keyword == "")
            {
                return true;
            }

            foreach (KeyValuePair<string, string> entry in keywordDict)
            {
                string iteratedKeyword = entry.Value;
                if (keyword.StartsWith(iteratedKeyword))
                {
                    return true;
                }
            }

            string message = $"{keyword} is invalid";
            string expectedValue = "The keyword should be listed in the format description";
            Error error = new Error(
                message,
                keyword,
                expectedValue,
                Filename,
                LineNumber,
                ErrorCode.INVALID_KEYWORD);

            ErrorValidationManager.Errors.Add(error);

            return false;
        }
    }
}
