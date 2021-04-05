using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Validation
{
    class PairSection
    {
        public bool[] ValidSectionPair { get; set; }
        public string OpeningSection { get; set; }
        public string ClosingSection { get; set; }

        public PairSection(string openingSection, string closingSection)
        {
            ValidSectionPair = new bool[2];
            ValidSectionPair[0] = false;
            ValidSectionPair[1] = false;
            OpeningSection = openingSection;
            ClosingSection = closingSection;
        }

        public bool CheckValidPair(ref ErrorManager errorManager, string fileName, string lineNumber)
        {
            if(ValidSectionPair[0] && ValidSectionPair[1])
            {
                return true;
            }

            string message = $"There is no configration data section";
            string actualValue = "null";
            string expectedValue = $"The section starts with {OpeningSection} and end with {ClosingSection}";
            Error error = new Error(message, actualValue, expectedValue, fileName, lineNumber);
            
            errorManager.Errors.Add(error);

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

        public bool StartWithOpeningSection(string line)
        {
            if (line.StartsWith(OpeningSection))
            {
                ValidSectionPair[0] = true;
                return true;
            }

            return false;
        }

        public bool StartWithClosingSection(string line)
        {
            if (line.StartsWith(ClosingSection))
            {
                ValidSectionPair[1] = true;
                return true;
            }

            return false;
        }
    }
}
