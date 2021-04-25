using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Components;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.DataStructure;

namespace TasksAllocation.Utils.FilesManipulation
{
    public class TaffAllocations
    {
        public int Count { get; set; }
        public int NumberOfTasks { get; set; }
        public int NumberOfProcessors { get; set; }
        public List<Allocation> Allocations { get; set; }
        private bool InsideAllocationData;
        private int AllocationID;
        private string AllocationMapData;


        public TaffAllocations()
        {
            Count = -1;
            NumberOfTasks = -1;
            NumberOfProcessors = -1;
            Allocations = new List<Allocation>();
            InsideAllocationData = false;
            AllocationID = -1;
            AllocationMapData = null;
        }

        public int ExtractCount(int count, string line, Validations validations)
        {
            if (count < 0 && line.StartsWith(TaffKeywords.ALLOCATIONS_COUNT))
            {
                Count = validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_COUNT);

                return Count;
            }

            return count;
        }

        public int ExtractNumOfTasks(int numOfTasks, string line, Validations validations)
        {
            if (numOfTasks < 0 && line.StartsWith(TaffKeywords.ALLOCATIONS_TASKS))
            {
                NumberOfTasks = validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_TASKS);

                return NumberOfTasks;
            }

            return numOfTasks;
        }

        public int ExtractNumOfProcessors(int numOfProcessors, string line, Validations validations)
        {
            if (numOfProcessors < 0 && line.StartsWith(TaffKeywords.ALLOCATIONS_PROCESSORS))
            {
                NumberOfTasks = validations.ValidateIntegerPair(
                        line,
                        TaffKeywords.ALLOCATIONS_PROCESSORS);

                return NumberOfTasks;
            }

            return numOfProcessors;
        }

        public void MarkInsideAllocation(string line, int lineNumber, Validations validations)
        {
            if (line == TaffKeywords.OPENING_ALLOCATION)
            {
                InsideAllocationData = true;
            }
  
            if (line == TaffKeywords.CLOSING_ALLOCATION)
            {
                // Check the required values are missing
                validations.CheckRequiredValueExist(AllocationID.ToString(), TaffKeywords.ALLOCATION_ID);
                validations.CheckRequiredValueExist(AllocationMapData, TaffKeywords.ALLOCATION_MAP);

                // Reset Allocation data
                InsideAllocationData = false;
                Count--;
                AllocationID = -1;
                AllocationMapData = null;
            }
        }

        private void ExtractID(string line, Validations validations)
        {
            if (AllocationID < 0 && line.StartsWith(TaffKeywords.ALLOCATION_ID))
            {
                AllocationID = validations.ValidateIntegerPair(
                    line,
                    TaffKeywords.ALLOCATION_ID);
            }
        }

        private void ExtractMapData(string line, Validations validations)
        {

            if (AllocationMapData == null && line.StartsWith(TaffKeywords.ALLOCATION_MAP))
            {
                // Check the MAP format is valid
                bool isValidMapFormat = RegexValidation.RegexMap(line, validations);

                if (isValidMapFormat)
                {
                    AllocationMapData = validations.ValidateStringPair(
                        line,
                        TaffKeywords.ALLOCATION_MAP);
                }
            }
        }

        public Allocation ExtractAllocation(string line, int numOfProcessors, int numOfTasks, Validations validations)
        {
            if (InsideAllocationData)
            {
                ExtractID(line, validations);
                ExtractMapData(line, validations);

                if (AllocationMapData != null && AllocationID >= 0)
                {
                    Map newMapData = new Map(AllocationMapData);
                    Allocation newAllocation = new Allocation(AllocationID, newMapData);

                    // Convert the map data to a 2D array
                    newAllocation.MapMatrix = newMapData.ConvertToMatrix(
                        numOfProcessors,
                        numOfTasks,
                        validations);

                    // Check the number of tasks in each allocation
                    int sumOfTasks = newAllocation.CountTasks();
                    bool validMapData = validations.CheckValidQuantity(
                        sumOfTasks.ToString(),
                        numOfTasks.ToString(),
                        "tasks in a allocation",
                        ErrorCode.INVALID_MAP);

                    if (validMapData)
                    {
                        Allocations.Add(newAllocation);
                        return newAllocation;
                    }
                }
            }
            return null;
        }

        public void ScanErrors(
            int count, 
            int numberOfTasks, 
            int numberOfProcessers,
            KeywordPair allocationPair,
            string taffFilename,
            Validations validations)
        {
            // Check TASKS, COUNT and PROCESSORS
            bool countExists = validations.CheckRequiredValueExist(
                count.ToString(),
                TaffKeywords.ALLOCATIONS_COUNT);
            bool taskExists = validations.CheckRequiredValueExist(
                numberOfTasks.ToString(),
                TaffKeywords.ALLOCATIONS_TASKS);
            bool processorsExists = validations.CheckRequiredValueExist(
                numberOfProcessers.ToString(),
                TaffKeywords.ALLOCATIONS_PROCESSORS);

            // Checking whether the number of allocation is the same as the defined COUNT
            if (countExists && taskExists && processorsExists)
            {
                validations.CheckValidQuantity(
                   allocationPair.CalculateNumOfPair().ToString(),
                   count.ToString(),
                   TaffKeywords.OPENING_ALLOCATION,
                   ErrorCode.MISSING_SECTION);

                validations.CheckValidQuantity(
                   Allocations.Count.ToString(),
                   count.ToString(),
                   TaffKeywords.OPENING_ALLOCATION,
                   ErrorCode.MISSING_SECTION);
            }
        }
    }
}
