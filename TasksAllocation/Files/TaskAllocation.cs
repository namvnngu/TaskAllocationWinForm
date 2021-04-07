using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TasksAllocation.Components;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.FilesManipulation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Files
{
    class TaskAllocation
    {
        public string CffFilename { get; set; }
        public int Count { get; set; }
        public int NumberOfTasks { get; set; }
        public int NumberOfProcessors { get; set; }
        public List<Allocation> Allocations { get; set; }

        public TaskAllocation()
        {
            CffFilename = null;
            Count = -1;
            NumberOfTasks = -1;
            NumberOfProcessors = -1;
            Allocations = new List<Allocation>();
        }

        public bool GetCffFilename(string taffFilename, Validations validations)
        {
            int beforeNumOfError, afterNumOfError;

            beforeNumOfError = validations.ErrorValidationManager.Errors.Count;

            CffFilename = TaffManipulation.ExtractCff(taffFilename, validations);

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }

        public bool Validate(string taffFilename, Validations validations)
        {
            if (taffFilename == null)
            {
                return false;
            }

            // Extract and validate the configuration data section
            GetCffFilename(taffFilename, validations);

            // Validate the rest of the taff file
            int beforeNumOfError, afterNumOfError, lineNumber = 1, allocationCount = -1;
            PairSection openClosingAllocations = new PairSection(
                TaffKeywords.OPENING_ALLOCATIONS,
                TaffKeywords.CLOSING_ALLOCATIONS);
            PairSection openClosingAllocation;
            string line;
            StreamReader streamReader = new StreamReader(taffFilename);
            validations.Filename = taffFilename;

            beforeNumOfError = validations.ErrorValidationManager.Errors.Count;

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();
                validations.LineNumber = lineNumber.ToString();

                // Check whether the line starts Opening/Closing Allocations section 
                // If yes, mark it exist
                openClosingAllocations.MarkSection(line, lineNumber);

                // Check whether Allocations section exists and
                // whether line strt with the expected keyword, "COUNT", "TASKS"
                // and "PROCESSORS"
                if (Count < 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.ALLOCATIONS_COUNT))
                {
                    Count = validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_COUNT);
                    allocationCount = Count;
                }

                if (NumberOfTasks < 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.ALLOCATIONS_TASKS))
                {
                    NumberOfTasks = validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_TASKS);
                }

                if (NumberOfProcessors < 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.ALLOCATIONS_PROCESSORS))
                {
                    NumberOfProcessors = validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_PROCESSORS);
                }

                // According to Count, extract the relevant allocation data
                /*
                if (allocationCount > 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.OPENING_ALLOCATION))
                {
                    string id = null;
                    string mapData = null;
                    openClosingAllocation = new PairSection(
                        TaffKeywords.OPENING_ALLOCATION,
                        TaffKeywords.CLOSING_ALLOCATION);

                    // Check whether the line starts Opening/Closing Allocation section 
                    // If yes, mark it exist
                    openClosingAllocation.MarkSection(line, lineNumber);

                    // Checking whether the Allocation section exists
                    openClosingAllocation.CheckValidPair(validations, taffFilename);

                    allocationCount--;
                }
                */
                lineNumber++;
            }

            // Checking whether the Allocations section exists
            openClosingAllocations.CheckValidPair(validations, taffFilename);

            streamReader.Close();

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }
    }
}
