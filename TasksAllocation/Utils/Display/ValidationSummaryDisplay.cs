using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TasksAllocation.Utils.Display
{
    public class ValidationSummaryDisplay
    {
        const string INVALID = "invalid";
        const string VALID = "valid";

        public static string ValidAllocationFile(string taffFile, bool validTaskAllocation)
        {
            StringBuilder text = new StringBuilder();
            string filename = ExtractFilename(taffFile);
            string fileStatus = validTaskAllocation ? VALID : INVALID;

            text.Append($"<div>Allocation file ({filename}) is {fileStatus}.</div>");

            return text.ToString();
        }

        public static string ValidConfigurationFile(string cffFile, bool validConfiguration)
        {
            StringBuilder text = new StringBuilder();
            string filename = ExtractFilename(cffFile);
            string fileStatus = validConfiguration ? VALID : INVALID;

            text.Append($"<div>Configuration file ({filename}) is {fileStatus}.</div>");

            return text.ToString();
        }

        public static string ValidAllocations(int errorCount)
        {
            StringBuilder text = new StringBuilder();
            string validAllocation = errorCount == 0 ? VALID : INVALID;

            text.Append($"<div>The set of allocations is {validAllocation}.</div>");

            return text.ToString();
        }

        private static string ExtractFilename(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
