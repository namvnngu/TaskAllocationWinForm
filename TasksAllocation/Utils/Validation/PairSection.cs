using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.Validation
{
    class PairSection
    {
        public bool[] ValidSectionPair { get; set; }
        public string OpeningSection { get; set; }
        public string ClosingSection { get; set; }
        private int OpeningLineNumber = 0;
        private int ClosingLineNumber = 0;

        public PairSection(string openingSection, string closingSection)
        {
            ValidSectionPair = new bool[2];
            ValidSectionPair[0] = false;
            ValidSectionPair[1] = false;
            OpeningSection = openingSection;
            ClosingSection = closingSection;
        }

        public bool CheckValidPair(Validations validations, string fileName)
        {
            if(ValidSectionPair[0] && ValidSectionPair[1])
            {
                return true;
            }

            string message = $"There is no configration data section or misspelled section keywords";
            string actualValue = "null";
            string expectedValue = $"The section starts with \"{OpeningSection}\" " +
                $"and ends with \"{ClosingSection}\"";
            string lineText = "";

            if (OpeningLineNumber != 0)
            {
                lineText = OpeningLineNumber.ToString();
            } else if (ClosingLineNumber != 0)
            {
                lineText = ClosingLineNumber.ToString();
            }

            Error error = new Error(
                message, 
                actualValue, 
                expectedValue, 
                fileName, 
                lineText, 
                ErrorCode.MISSING_SECTION);
            
            validations.ErrorValidationManager.Errors.Add(error);

            return false;
        }

        public bool SetValidOpeningSection(bool condition)
        {
            if (condition)
            {
                ValidSectionPair[0] = true;
                return true;
            }

            return false;
        }

        public bool SetValidClosingSection(bool condition)
        {
            if (condition)
            {
                ValidSectionPair[1] = true;
                return true;
            }

            return false;
        }

        public bool StartWithOpeningSection(string line, int lineNumber)
        {
            if (line == OpeningSection)
            {
                OpeningLineNumber = lineNumber;
                ValidSectionPair[0] = true;
                return true;
            }

            return false;
        }

        public bool StartWithClosingSection(string line, int lineNumber)
        {
            if (line == ClosingSection)
            {
                ClosingLineNumber = lineNumber;
                ValidSectionPair[1] = true;
                return true;
            }

            return false;
        }

        public void MarkSection(string line, int lineNumber)
        {
            StartWithOpeningSection(line, lineNumber);

            if (!ValidSectionPair[0] || !ValidSectionPair[1])
            {
                if (ValidSectionPair[0])
                {
                    StartWithClosingSection(line, lineNumber);
                }
            }
        }
    }
}
