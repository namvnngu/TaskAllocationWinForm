using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Display
{
    class ErrorDisplay
    {
        public static string DisplayText(List<string[]> errors)
        {
            string renderedText = Display.BOOTSTRAP_LINK;
            
            renderedText += $"<h3 class=\"font-weight-bold\">There are <span class=\"badge badge-primary badge-pill\">{errors.Count}</span> errors</h3>";
            
            for (int errorNumber = 0; errorNumber < errors.Count; errorNumber++)
            {
                string[] error = errors[errorNumber];

                renderedText += $"<div class=\"text-danger\">Error {errorNumber + 1}</div>";
                renderedText += $"<div>Message: {error[0]}</div>";
                renderedText += $"<div>Error: {error[1]}</div>";
                renderedText += $"<div>Expected: {error[2]}</div><br/>";
            }

            return renderedText;
        }
    }
}
