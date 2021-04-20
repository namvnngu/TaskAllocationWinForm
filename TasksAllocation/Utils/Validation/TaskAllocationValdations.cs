using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Components;
using TasksAllocation.Utils.Display;
using TasksAllocation.Files;

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

        public bool ValidateTaskRAM(AllocationDisplay allocationDisplay, Configuration configuration)
        {
            int errorCount = 0;
            List<ProcessorAllocation> processorAllocations = allocationDisplay.ProcessorAllocations;

            for (int processorAllocatioNum = 0; processorAllocatioNum < processorAllocations.Count; processorAllocatioNum++)
            {
                ProcessorAllocation processorAllocation = processorAllocations[processorAllocatioNum];
                int processorAllocationRAM = processorAllocation.RAM;
                int processorRAM = configuration.Processors[processorAllocatioNum].RAM;

                if (processorAllocationRAM > processorRAM)
                {

                    Error error = new Error();

                    error.Message = $"The processor (ID={processorAllocatioNum}) of allocation (ID={allocationDisplay.ID}) " +
                        $"has {processorRAM} GB RAM but requires {processorAllocationRAM} GB RAM";
                    error.ActualValue = $"{processorAllocationRAM} GB RAM";
                    error.ExpectedValue = $"Must be less than or equal to {processorRAM} GB RAM";
                    error.Filename = ValidationsManager.Filename;
                    error.LineNumber = "";
                    error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                    errorCount++;
                    ValidationsManager.ErrorValidationManager.Errors.Add(error);
                }

            }

            return (errorCount == 0);
        }

        public bool ValidateTaskUpload(AllocationDisplay allocationDisplay, Configuration configuration)
        {
            int errorCount = 0;
            List<ProcessorAllocation> processorAllocations = allocationDisplay.ProcessorAllocations;

            for (int processorAllocatioNum = 0; processorAllocatioNum < processorAllocations.Count; processorAllocatioNum++)
            {
                ProcessorAllocation processorAllocation = processorAllocations[processorAllocatioNum];
                int processorAllocationUpload = processorAllocation.Upload;
                int processorUpload = configuration.Processors[processorAllocatioNum].Upload;

                if (processorAllocationUpload > processorUpload)
                {

                    Error error = new Error();

                    error.Message = $"The processor (ID={processorAllocatioNum}) of allocation (ID={allocationDisplay.ID}) " +
                        $"has a maximum upload {processorUpload} Gbps but requires {processorAllocationUpload} Gbps";
                    error.ActualValue = $"{processorAllocationUpload} Gbps";
                    error.ExpectedValue = $"Must be less than or equal to {processorUpload} Gbps";
                    error.Filename = ValidationsManager.Filename;
                    error.LineNumber = "";
                    error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                    errorCount++;
                    ValidationsManager.ErrorValidationManager.Errors.Add(error);
                }

            }

            return (errorCount == 0);
        }

        public bool ValidateTaskDownload(AllocationDisplay allocationDisplay, Configuration configuration)
        {
            int errorCount = 0;
            List<ProcessorAllocation> processorAllocations = allocationDisplay.ProcessorAllocations;

            for (int processorAllocatioNum = 0; processorAllocatioNum < processorAllocations.Count; processorAllocatioNum++)
            {
                ProcessorAllocation processorAllocation = processorAllocations[processorAllocatioNum];
                int processorAllocationDownload = processorAllocation.Download;
                int processorDownload = configuration.Processors[processorAllocatioNum].Download;

                if (processorAllocationDownload > processorDownload)
                {

                    Error error = new Error();

                    error.Message = $"The processor (ID={processorAllocatioNum}) of allocation (ID={allocationDisplay.ID}) " +
                        $"has a maximum download {processorDownload} Gbps but requires {processorAllocationDownload} Gbps";
                    error.ActualValue = $"{processorAllocationDownload} Gbps";
                    error.ExpectedValue = $"Must be less than or equal to {processorDownload} Gbps";
                    error.Filename = ValidationsManager.Filename;
                    error.LineNumber = "";
                    error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                    errorCount++;
                    ValidationsManager.ErrorValidationManager.Errors.Add(error);
                }

            }

            return (errorCount == 0);
        }

        public bool IsAllocationEnergiesEqual(double initalEnergy, AllocationDisplay allocationDisplay)
        {
            int errorCount = 0;
            double allocationEnergy = allocationDisplay.Energy;

            if (allocationEnergy != initalEnergy)
            {
                Error error = new Error();

                error.Message = $"The energy value ({allocationEnergy}) of an allocation (ID={allocationDisplay.ID})" +
                    $" differs from the energy value ({initalEnergy}) of another allocation (ID=0)";
                error.ActualValue = $"{allocationEnergy}";
                error.ExpectedValue = "All allocation's energy values must be the same";
                error.Filename = ValidationsManager.Filename;
                error.LineNumber = "";
                error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                errorCount++;
                ValidationsManager.ErrorValidationManager.Errors.Add(error);
            }

            return (errorCount == 0);
        }

        public bool CheckValidRuntime(AllocationDisplay allocationDisplay, double requiredRuntime)
        {
            double runtime = allocationDisplay.Runtime;
            int errorCount = 0;

            if (runtime > requiredRuntime)
            {
                Error error = new Error();

                error.Message = $"The runtime ({runtime}) of an allocation (ID={allocationDisplay.ID})" +
                    $" is greater then the expected program runtime ({requiredRuntime})";
                error.ActualValue = $"{runtime}";
                error.ExpectedValue = $"Must be smaller than or equal to {requiredRuntime}";
                error.Filename = ValidationsManager.Filename;
                error.LineNumber = "";
                error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                errorCount++;
                ValidationsManager.ErrorValidationManager.Errors.Add(error);
            }

            return (errorCount == 0);
        }
    }
}
