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
using TasksAllocation.Utils.DataStructure;

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
            PairSection openClosingAllocations = new PairSection(
                TaffKeywords.OPENING_ALLOCATIONS,
                TaffKeywords.CLOSING_ALLOCATIONS);
            PairSection openClosingAllocation;
            List<PairSection> allocationSectionList = new List<PairSection>();
            StreamReader streamReader = new StreamReader(taffFilename);
            int beforeNumOfError, afterNumOfError, lineNumber = 1;
            int id = -1, allocationCount = -1;
            string line, mapData = null;
            bool insideAllocationData = false;

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

                    for (int countNum = 0; countNum < Count; countNum++)
                    {
                        openClosingAllocation = new PairSection(
                            TaffKeywords.OPENING_ALLOCATION,
                            TaffKeywords.CLOSING_ALLOCATION);
                        allocationSectionList.Add(openClosingAllocation);
                    }

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
                if (allocationCount > 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    allocationSectionList[allocationCount - 1].StartWithOpeningSection(line, lineNumber))
                {
                    insideAllocationData = true;
                }

                if (allocationCount > 0 &&
                    openClosingAllocations.ValidSectionPair[0] &&
                    allocationSectionList[allocationCount - 1].StartWithClosingSection(line, lineNumber))
                {
                    // Reset value and decrement the number of allocation
                    insideAllocationData = false;
                    allocationCount--;
                    id = -1;
                    mapData = null;
                }

                if (insideAllocationData)
                {
                    if (id < 0 && line.StartsWith(TaffKeywords.ALLOCATION_ID))
                    {
                        id = validations.ValidateIntegerPair(
                            line,
                            TaffKeywords.ALLOCATION_ID);
                    }

                    if (mapData == null && line.StartsWith(TaffKeywords.ALLOCATION_MAP))
                    {
                        mapData = validations.ValidateStringPair(
                            line,
                            TaffKeywords.ALLOCATION_MAP);
                    }

                    if (mapData != null && id >= 0)
                    {
                        Map newMapData = new Map(mapData);
                        Allocation newAllocation = new Allocation(id, newMapData);

                        // Convert the map data to a 2D array
                        newAllocation.MapMatrix = newMapData.ConvertToMatrix(
                            NumberOfProcessors,
                            NumberOfTasks,
                            validations);

                        // Check the number of tasks in each allocation
                        int sumOfTasks = newAllocation.CountTasks();
                        bool validMapData = validations.CheckValidQuantity(
                            sumOfTasks.ToString(),
                            NumberOfTasks.ToString(),
                            "tasks in a allocation",
                            ErrorCode.INVALID_MAP);

                        if (validMapData)
                        {
                            Allocations.Add(newAllocation);
                        }
                    }
                }

                lineNumber++;
            }

            streamReader.Close();

            // Checking whether the Allocations section exists
            openClosingAllocations.CheckValidPair(validations, taffFilename);

            // Checking whether the number of allocation is the same as the defined COUNT
            validations.CheckValidQuantity(
                Allocations.Count.ToString(),
                Count.ToString(),
                TaffKeywords.OPENING_ALLOCATION,
                ErrorCode.MISSING_SECTION);

            // Check whether the Allocation sections exist
            foreach (PairSection pairSection in allocationSectionList)
            {
                Console.WriteLine($"{pairSection.ValidSectionPair[0]} | {pairSection.ValidSectionPair[0]}");
                pairSection.CheckValidPair(validations, taffFilename);
            }

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }
    }
}
