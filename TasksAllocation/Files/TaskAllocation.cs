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

        public bool GetCffFilename(string taffFilename, ref ErrorManager errorManager)
        {
            int beforeNumOfError, afterNumOfError;

            beforeNumOfError = errorManager.Errors.Count;

            CffFilename = TaffManipulation.ExtractCff(taffFilename, ref errorManager);

            afterNumOfError = errorManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }

        public bool Validate(string taffFilename, ref ErrorManager errorManager)
        {
            int beforeNumOfError, afterNumOfError, lineNumber = 1, allocationCount = -1;
            PairSection openClosingAllocations = new PairSection(
                TaffKeywords.OPENING_ALLOCATIONS,
                TaffKeywords.CLOSING_ALLOCATIONS);
            PairSection openClosingAllocation;
            string line;
            StreamReader streamReader = new StreamReader(taffFilename);

            beforeNumOfError = errorManager.Errors.Count;
            Count = -1;
            NumberOfTasks = -1;
            NumberOfProcessors = -1;

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();

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
                    Count = Validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_COUNT,
                        ref errorManager,
                        taffFilename,
                        lineNumber.ToString());
                    allocationCount = Count;
                }

                if (NumberOfTasks < 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.ALLOCATIONS_TASKS))
                {
                    NumberOfTasks = Validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_TASKS,
                        ref errorManager,
                        taffFilename,
                        lineNumber.ToString());
                }

                if (NumberOfProcessors < 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.ALLOCATIONS_PROCESSORS))
                {
                    NumberOfProcessors = Validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_PROCESSORS,
                        ref errorManager,
                        taffFilename,
                        lineNumber.ToString());
                }

                // According to Count, extract the relevant allocation data
                
                if (allocationCount > 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    line.StartsWith(TaffKeywords.OPENING_ALLOCATION))
                {
                    Allocations = new List<Allocation>();
                    string id = null;
                    string mapData = null;
                    openClosingAllocation = new PairSection(
                        TaffKeywords.OPENING_ALLOCATION,
                        TaffKeywords.CLOSING_ALLOCATION);

                    // Check whether the line starts Opening/Closing Allocation section 
                    // If yes, mark it exist
                    openClosingAllocation.MarkSection(line, lineNumber);

                    // Checking whether the Allocation section exists
                    openClosingAllocation.CheckValidPair(ref errorManager, taffFilename);

                    allocationCount--;
                }
                lineNumber++;
            }

            // Checking whether the Allocations section exists
            openClosingAllocations.CheckValidPair(ref errorManager, taffFilename);

            streamReader.Close();

            afterNumOfError = errorManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }
    }
}
