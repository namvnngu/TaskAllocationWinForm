using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.Validation
{
    class RegexValidation
    {
        public static bool RegexExtension(string text, Validations validations)
        {
            string pattern = @"(\.\w+$)";
            bool exisitingExtension = Regex.IsMatch(text, pattern);

            if (!exisitingExtension)
            {
                string message = "Invalid file extension format";
                string actualValue = text;
                string expectedValue = $"Regular Expression = {pattern}";

                AddError(
                    message,
                    actualValue,
                    expectedValue,
                    validations);
            }
            
            return exisitingExtension;
        }

        public static bool RegexIntegerPair(string text, Validations validations)
        {
            string pattern = @"^\s*(\w+(\-?\w+)*)=(\d+)$";
            bool validPair = Regex.IsMatch(text, pattern);

            if (!validPair)
            {
                string message = "Invalid key-value (integer value) format";
                string actualValue = text;
                string expectedValue = $"Regular Expression = {pattern}";

                AddError(
                    message,
                    actualValue,
                    expectedValue,
                    validations);
            }

            return validPair;
        }

        public static bool RegexDoublePair(string text, Validations validations)
        {
            string pattern = @"^\s*(\w+(\-?\w+)*)=(\d+([.|,]\d+)?)$";
            bool validPair = Regex.IsMatch(text, pattern);

            if (!validPair)
            {
                string message = "Invalid key-value (floating number) format";
                string actualValue = text;
                string expectedValue = $"Regular Expression = {pattern}";

                AddError(
                    message,
                    actualValue,
                    expectedValue,
                    validations);
            }

            return validPair;
        }

        public static bool RegexPair(string text, Validations validations)
        {
            string pattern = @"^\s*(\w+(\-?\w+)*)=(.+)$";
            bool validPair = Regex.IsMatch(text, pattern);

            if (!validPair)
            {
                string message = "Invalid key-value format";
                string actualValue = text;
                string expectedValue = $"Regular Expression = {pattern}";

                AddError(
                    message,
                    actualValue,
                    expectedValue,
                    validations);
            }

            return validPair;
        }

        public static bool RegexMap(string text, Validations validations)
        {
            string pattern = @"^\s*MAP=((0|1)(,0|,1)*)(;(0|1)(,0|,1)*)*$";
            bool validPair = Regex.IsMatch(text, pattern);

            if (!validPair)
            {
                string message = "Invalid key-value (MAP) format";
                string actualValue = text;
                string expectedValue = $"Regular Expression = {pattern}";

                AddError(
                    message,
                    actualValue,
                    expectedValue,
                    validations);
            }

            return validPair;
        }

        public static void AddError(string message, string actualValue, string expectedValue, Validations validations)
        {
            Error error = new Error();

            error.Message = message;
            error.ActualValue = actualValue;
            error.ExpectedValue = expectedValue;
            error.Filename = validations.Filename;
            error.LineNumber = validations.LineNumber;
            error.ErrorCode = ErrorCode.INVALID_FORMAT;

            validations.ErrorValidationManager.Errors.Add(error);
        }
    }
}
