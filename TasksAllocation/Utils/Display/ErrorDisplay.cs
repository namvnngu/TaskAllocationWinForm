using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.Display
{
    class ErrorDisplay
    {
        public static string DisplayText(List<Error> errors)
        {
            string renderedText = "";
            int numOfTaffErrors = CountTaffErrors(errors);
            int numOfCffErrors = errors.Count - numOfTaffErrors;
            
            renderedText += $"<h3>There are " +
                $"<span style=\" color: red \">{errors.Count}</span> errors" +
                $", where TAFF file has <span style=\" color: red \">{numOfTaffErrors}</span> errors and " +
                $"CFF file has <span style=\" color: red \">{numOfCffErrors}</span> errors</h3>";
            
            for (int errorNumber = 0; errorNumber < errors.Count; errorNumber++)
            {
                Error error = errors[errorNumber];
                string lineNumberText = error.LineNumber != "" ? $"Line {error.LineNumber}:" : "File:";
                string errorCodeDescription = ErrorCode.ErrorCodeDescription[error.ErrorCode];
                string actualValue = error.ActualValue == "-1" || error.ActualValue == "0" ? "null" : error.ActualValue;

                renderedText += $"<div style=\" color: red \">Error {error.ErrorCode}: {errorCodeDescription}</div>";
                renderedText += $"<div style=\" color: red \">{lineNumberText} <span style=\" color: blue \">{error.Filename}</span></div>";
                renderedText += $"<div>Message: {error.Message}</div>";
                renderedText += $"<div>Actual value: {actualValue}</div>";
                renderedText += $"<div>Expected value: {error.ExpectedValue}</div><br>";
            }

            return renderedText;
        }

        public static int CountTaffErrors(List<Error> errors)
        {
            int numOfTafffErrors = 0;
            string taffFilename = errors[0].Filename;

            for (int errorNumber = 0; errorNumber < errors.Count; errorNumber++)
            {
                Error error = errors[errorNumber];
                string currentFilename = error.Filename;
                if (currentFilename != taffFilename)
                {
                    break;
                }

                numOfTafffErrors++;
            }

            return numOfTafffErrors;
        }
    }
}
