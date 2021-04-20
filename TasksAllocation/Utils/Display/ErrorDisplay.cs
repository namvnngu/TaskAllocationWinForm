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
        public static string DisplayText(ErrorManager errorManager)
        {
            string renderedText = "";
            Dictionary<string, List<Error>> errorTypeDict = errorManager.ClassifyFileError();
            List<Error> taffErrors = errorTypeDict[TaffKeywords.FILE_EXTENSION];
            List<Error> cffErrors = errorTypeDict[CffKeywords.FILE_EXTENSION];
            int numOfTaffErrors = taffErrors.Count;
            int numOfCffErrors = cffErrors.Count;
            int totalError = numOfCffErrors + numOfTaffErrors;
            
            renderedText += $"<h3>There are " +
                $"<span style=\" color: red \">{totalError}</span> errors" +
                $", where TAFF file has <span style=\" color: red \">{numOfTaffErrors}</span> errors and " +
                $"CFF file has <span style=\" color: red \">{numOfCffErrors}</span> errors</h3>";

            renderedText += RenderErrorText(taffErrors, "ALLOCATION");
            renderedText += RenderErrorText(cffErrors, "CONFIGURATION");

            return renderedText;
        }

        public static string RenderErrorText(List<Error> errors, string fileType)
        {
            string renderedText = "";

            if (errors.Count == 0)
            {
                return renderedText;
            }

            renderedText += $"<h4 style=\" color: green \">START PROCESSING {fileType} FILE: {errors[0].Filename}</h4>";

            for (int errorNumber = 0; errorNumber < errors.Count; errorNumber++)
            {
                Error error = errors[errorNumber];
                string lineNumberText = error.LineNumber != "" ? $"Line {error.LineNumber}:" : "File:";
                string errorCodeDescription = ErrorCode.ErrorCodeDescription[error.ErrorCode];
                string actualValue = error.ActualValue == "-1" || error.ActualValue == "0" ? "null" : error.ActualValue;

                renderedText += $"<div style=\" color: red \">ERROR {error.ErrorCode}: {errorCodeDescription}</div>";
                renderedText += $"<div style=\" color: red \">{lineNumberText} <span style=\" color: blue \">{error.Filename}</span></div>";
                renderedText += $"<div>Message: {error.Message}</div>";
                renderedText += $"<div>Actual value: {actualValue}</div>";
                renderedText += $"<div>Expected value: {error.ExpectedValue}</div><br>";
            }

            renderedText += $"<h4 style=\" color: green \">END PROCESSING {fileType} FILE: {errors[0].Filename}</h4>";

            return renderedText;
        }
    }
}
