using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.FilesManipulation
{
    class CffLogFile
    {
        public PairSection LogFileSection { get; set; }
        public string LogFile { get; set; }
        public string LOGFILE_EXTENSION = "txt";

        public CffLogFile()
        {
            LogFileSection = new PairSection(
                CffKeywords.OPENING_LOGFILE,
                CffKeywords.CLOSING_LOGFILE);
            LogFile = null;
        }

        public string ExtractLogFile(string cffFilename, string line, Validations validations)
        {
            string EXPECTED_LOGFILE_FORMAT = $"\"[name].{LOGFILE_EXTENSION}\"";

            // Check whether the line starts opening/closing LOGFILE section
            // If yes, mark it exist
            LogFileSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            // Check the line start with the expected keyword, "DEFAULT"
            if (line.StartsWith(CffKeywords.DEFAULT_LOGFILE))
            {
                // Check whether the pair key-value exists
                string[] lineData = validations.CheckPairKeyValue(
                    line,
                    CffKeywords.DEFAULT_LOGFILE,
                    EXPECTED_LOGFILE_FORMAT);

                if (lineData != null)
                {
                    LogFile = validations.CheckTextValueExist(
                        lineData[1],
                        LogFile,
                        EXPECTED_LOGFILE_FORMAT);
                }
            }

            // Check whether the value in the above pair has the valid extension (.text),
            // and check the filename has no invalia file character
            bool logfileExtensionValid = LogFile != null &&
                validations.CheckExtension(LogFile, LOGFILE_EXTENSION);
            bool noInvalidFilenameChars = logfileExtensionValid &&
                validations.CheckInvalidFileNameChars(LogFile);

            return LogFile;
        }
    }
}
