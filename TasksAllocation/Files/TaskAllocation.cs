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
        private string TaffFilename = "";

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

        public bool ValidateFile(string taffFilename, Validations validations)
        {
            if (taffFilename == null)
            {
                return false;
            }

            int beforeNumOfError, afterNumOfError;

            beforeNumOfError = validations.ErrorValidationManager.Errors.Count;
            TaffFilename = taffFilename;

            // Extract and validate the configuration data section
            GetCffFilename(taffFilename, validations);

            // Validate the rest of the taff file
            KeywordPair allocationPair = new KeywordPair(
                TaffKeywords.OPENING_ALLOCATION,
                TaffKeywords.CLOSING_ALLOCATION);
            PairSection openClosingAllocations = new PairSection(
                TaffKeywords.OPENING_ALLOCATIONS,
                TaffKeywords.CLOSING_ALLOCATIONS);
            StreamReader streamReader = new StreamReader(taffFilename);
            TaffAllocations taffAllocations = new TaffAllocations();
            int lineNumber = 1;
            string line;

            validations.Filename = taffFilename;

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();
                validations.LineNumber = lineNumber.ToString();

                // Check if keyword is valid or not
                validations.CheckValidKeyword(line, TaffKeywords.KEYWORD_DICT);

                // Count Allocation Section
                allocationPair.CheckValidKeyword(line);

                // Check whether the line starts Opening/Closing Allocations section 
                // If yes, mark it exist
                openClosingAllocations.MarkSection(line, lineNumber);

                // Validate and extract data in the Allocations section
                if (openClosingAllocations.ValidSectionPair[0] &&
                    !openClosingAllocations.ValidSectionPair[1])
                {
                    // Check whether Allocations section exists and
                    // whether line strt with the expected keyword, "COUNT", "TASKS"
                    // and "PROCESSORS"
                    Count = taffAllocations.ExtractCount(Count, line, validations);
                    NumberOfTasks = taffAllocations.ExtractNumOfTasks(NumberOfTasks, line, validations);
                    NumberOfProcessors = taffAllocations.ExtractNumOfProcessors(NumberOfProcessors, line, validations);

                    // Check whether the reader goes within the Allocation section
                    taffAllocations.MarkInsideAllocation(line, lineNumber, validations);

                    // Extract new allocation
                    Allocation newAllocation = taffAllocations.ExtractAllocation(line, NumberOfProcessors, NumberOfTasks, validations);

                    if (newAllocation != null)
                    {
                        Allocations.Add(newAllocation);
                    }
                }

                lineNumber++;
            }

            streamReader.Close();

            // Checking whether the Allocations section exists
            openClosingAllocations.CheckValidPair(validations, taffFilename);

            // Validate Allocation
            taffAllocations.ScanErrors(
                Count,
                NumberOfTasks,
                NumberOfProcessors,
                allocationPair,
                taffFilename,
                validations);

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            // Console.WriteLine($"{Count} | {NumberOfTasks} | {NumberOfProcessors}");
            // Console.WriteLine($"{Allocations.Count} | {allocationPair.CalculateNumOfPair()}");

            return (beforeNumOfError == afterNumOfError);
        }

        public bool ValidateAllocations(Configuration configuration, Validations validations)
        {
            int beforeNumOfError, afterNumOfError;
            TaskAllocationValdations taskAllocationValdations = new TaskAllocationValdations(validations);

            taskAllocationValdations.ValidationsManager.Filename = TaffFilename;
            beforeNumOfError = validations.ErrorValidationManager.Errors.Count;

            // Check the number of tasks and processors are the same in both cff and taff file
            taskAllocationValdations.IsEqual("PROCESSORS", NumberOfProcessors, configuration.Program.Processors);
            taskAllocationValdations.IsEqual("TASKS", NumberOfTasks, configuration.Program.Tasks);

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }
    }
}
