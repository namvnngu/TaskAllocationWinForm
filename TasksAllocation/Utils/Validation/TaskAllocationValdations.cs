using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Components;

namespace TasksAllocation.Utils.Validation
{
    class TaskAllocationValdations
    {
        public Validations ValidationsManager;

        public TaskAllocationValdations()
        {

        }

        public TaskAllocationValdations(Validations validations)
        {
            ValidationsManager = validations;
        }

        public bool IsEqual(string keyword, int taffValue, int cffValue)
        {
            if (taffValue != cffValue)
            {
                Error error = new Error();

                error.Message = $"The values of {keyword} in taff and cff file are not equal";
                error.ActualValue = $"{keyword} in taff is {taffValue}, and cff is {cffValue}";
                error.ExpectedValue = $"The {keyword} values in taff and cff file are the same";
                error.Filename = ValidationsManager.Filename;
                error.LineNumber = "";
                error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                ValidationsManager.ErrorValidationManager.Errors.Add(error);

                return false;
            }

            return true;
        }

        public bool CheckTaskExistsInOneProcessor(List<Allocation> allocations)
        {
            int errorCount = 0;

            foreach (Allocation allocation in allocations)
            {
                string[,] mapMatrix = allocation.MapMatrix;
                int nRow = mapMatrix.GetLength(0); // number of Processors
                int nCol = mapMatrix.GetLength(1); // number of Tasks
                int[] allocatedTasks = new int[nCol + 1];
                string TASK_ON = "1";

                for (int row = 0; row < nRow; row++)
                {
                    for (int col = 0; col < nCol; col++)
                    {
                        string task = mapMatrix[row, col];

                        if (task == TASK_ON)
                        {
                            allocatedTasks[col]++;
                        }

                        if (task == TASK_ON && allocatedTasks[col] > 1)
                        { 
                            Error error = new Error();

                            error.Message = $"The task (ID={col}) in allocation " +
                                $"(ID={allocation.ID}) is allocated in more than one processor";
                            error.ActualValue = $"The task (ID={col}) is allocated in {allocatedTasks[col]} proccessor(s)";
                            error.ExpectedValue = $"The task (ID={col}) must be allocated in only one processor";
                            error.Filename = ValidationsManager.Filename;
                            error.LineNumber = "";
                            error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                            errorCount++;
                            ValidationsManager.ErrorValidationManager.Errors.Add(error);
                        }
                    }
                }
            }

            return (errorCount == 0);
        }
    }
}
