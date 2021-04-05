using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation.Utils.Display
{
    class ErrorDisplay
    {
        public static string DisplayText(List<Error> errors)
        {
            string renderedText = Display.BOOTSTRAP_LINK;
            
            renderedText += $"<h3 class=\"font-weight-bold\">There are <span class=\"badge badge-primary badge-pill\">{errors.Count}</span> errors</h3>";
            
            for (int errorNumber = 0; errorNumber < errors.Count; errorNumber++)
            {
                Error error = errors[errorNumber];
                string lineNumberText = error.LineNumber != "" ? $", line {error.LineNumber}" : "";
                
                renderedText += $"<div class=\"text-danger\">Error {errorNumber + 1}: {error.Filename}{lineNumberText}</div>";
                renderedText += $"<div>Message: {error.Message}</div>";
                renderedText += $"<div>Actual value: {error.ActualValue}</div>";
                renderedText += $"<div>Expected value: {error.ExpectedValue}</div><br/>";
            }

            return renderedText;
        }
    }
}
