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
                AddError(
                    "Invalid file extension format",
                    text,
                    $"Regular Expression = {pattern}",
                    validations);
            }
            
            return exisitingExtension;
        }

        public static bool RegexIntegerPair(string text, Validations validations)
        {
            string pattern = @"(\w+)=(\d+)";
            bool validPair = Regex.IsMatch(text, pattern);

            if (!validPair)
            {
                AddError(
                    "Invalid key-value (integer value) format",
                    text,
                    $"Regular Expression = {pattern}",
                    validations);
            }

            return validPair;
        }

        public static bool RegexPair(string text, Validations validations)
        {
            string pattern = @"(\w+)=(.+)";
            bool validPair = Regex.IsMatch(text, pattern);

            if (!validPair)
            {
                AddError(
                    "Invalid key-value format",
                    text,
                    $"Regular Expression = {pattern}",
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
