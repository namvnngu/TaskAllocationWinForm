using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TasksAllocation.Components;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.FilesManipulation;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Utils.Display;

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
        public List<AllocationDisplay> AllocationDisplays { get; set; }

        public TaskAllocation()
        {
            CffFilename = null;
            Count = -1;
            NumberOfTasks = -1;
            NumberOfProcessors = -1;
            Allocations = new List<Allocation>();
            AllocationDisplays = new List<AllocationDisplay>();
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

            // Ensure one task is allocated in only one processor
            taskAllocationValdations.CheckTaskExistsInOneProcessor(Allocations);

            // Calculate allocations' Runtime and Energy
            // CalculateAllocationValues(configuration);

            foreach (AllocationDisplay allocationDisplay in AllocationDisplays)
            {
                // Ensure all allocation runtime is not greater than the expected program runtime
                double expectedProgramRuntime = configuration.Program.Duration;
                taskAllocationValdations.CheckValidRuntime(allocationDisplay, expectedProgramRuntime);

                // Ensure all allocations have the same energy
                double initalEnergy = AllocationDisplays[0].Energy;
                taskAllocationValdations.IsAllocationEnergiesEqual(initalEnergy, allocationDisplay);

                // Ensure the maximum amount of RAM required by tasks on each processor
                taskAllocationValdations.ValidateTaskRAM(allocationDisplay, configuration);

                // Ensure the maximum download required by tasks on each processor
                taskAllocationValdations.ValidateTaskDownload(allocationDisplay, configuration);

                // Ensure the maximum upload required by tasks on each processor
                taskAllocationValdations.ValidateTaskUpload(allocationDisplay, configuration);
            }
           

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }

        public void CalculateAllocationValues(Configuration configuration)
        {
            for (int allocationNum = 0; allocationNum < Allocations.Count; allocationNum++)
            {
                AllocationDisplay allocationDisplay = new AllocationDisplay();
                Allocation allocation = Allocations[allocationNum];

                allocationDisplay.ID = allocation.ID;
                allocationDisplay.Runtime = CalculateAllocationRuntime(allocation, configuration);
                allocationDisplay.Energy = CalculateAllocationEnergy(allocation, configuration);
                allocationDisplay.ProcessorAllocations = CalculateProcessorAllocationValues(allocation, configuration);

                AllocationDisplays.Add(allocationDisplay);
            }
        }


        public double CalculateAllocationRuntime(Allocation allocation, Configuration configuration)
        {
            string[,] mapMatrix = allocation.MapMatrix;
            int nRow = mapMatrix.GetLength(0);
            int nCol = mapMatrix.GetLength(1);
            string TASK_ON = "1";

            for (int row = 0; row < nRow; row++)
            {
                double currentProcessorRuntime = 0;
                for (int col = 0; col < nCol; col++)
                {
                    string task = mapMatrix[row, col];
                    if (task == TASK_ON)
                    {
                        Task currentTask = configuration.Tasks[col];
                        Processor currentProcessor = configuration.Processors[row];
                        double currentProcessorFrequency = currentProcessor.Frequency;

                        currentProcessorRuntime += currentTask.CalculateRuntime(currentProcessorFrequency);
                    }
                }

                allocation.Runtime = Math.Max(allocation.Runtime, currentProcessorRuntime);
            }

            allocation.Runtime = Math.Round(allocation.Runtime, 2);

            return allocation.Runtime;
        }

        public double CalculateAllocationEnergy(Allocation allocation, Configuration configuration)
        {
            double taskEnery = CalculateTaskEnergy(allocation, configuration);
            double communincationEnergy = CalculateCommunicationEnergy(allocation, configuration);

            allocation.Energy += taskEnery;
            allocation.Energy += communincationEnergy;

            allocation.Energy = Math.Round(allocation.Energy, 2);

            return allocation.Energy;
        }

        private double CalculateTaskEnergy(Allocation allocation, Configuration configuration)
        {
            string[,] mapMatrix = allocation.MapMatrix;
            int nRow = mapMatrix.GetLength(0);
            int nCol = mapMatrix.GetLength(1);
            string TASK_ON = "1";
            double taskEnergy = 0;

            for (int row = 0; row < nRow; row++)
            {
                for (int col = 0; col < nCol; col++)
                {
                    string task = mapMatrix[row, col];
                    if (task == TASK_ON)
                    {
                        Task currentTask = configuration.Tasks[col];
                        Processor currentProcessor = configuration.Processors[row];
                        double currentProcessorFrequency = currentProcessor.Frequency;
                        double currentTaskRuntime = currentTask.CalculateRuntime(currentProcessorFrequency);
                        double currenttaskEnergy = currentProcessor.PType.CalculateEnergy(currentProcessorFrequency, currentTaskRuntime);

                        taskEnergy += currenttaskEnergy;
                    }
                }
            }

            return taskEnergy;
        }

        private double CalculateCommunicationEnergy(Allocation allocation, Configuration configuration)
        {
            string[,] mapMatrix = allocation.MapMatrix;
            int numOfProcessors = mapMatrix.GetLength(0); // Number of rows
            int numOfTasks = mapMatrix.GetLength(1); // Number of columns
            string TASK_ON = "1";
            double localCommunicationEnergy = 0;
            double remoteCommunicationEnergy = 0;
            double communicationEnergy = 0;

            for (int row = 0; row < numOfProcessors; row++)
            {
                // Local Communication: Gain all tasks in the same processor
                List<int> currentLocalTasks = new List<int>();

                for (int col = 0; col < numOfTasks; col++)
                {
                    string task = mapMatrix[row, col];

                    if (task == TASK_ON)
                    {
                        currentLocalTasks.Add(col);
                    }
                }

                localCommunicationEnergy += CalculateLocalCommnucationEnergy(currentLocalTasks, configuration.LocalCommunicationInfo);

                // Remote Communication: For each the local task in the same processor,
                // find the external tasks which are in the different processors
                for (int localTaskNum = 0; localTaskNum < currentLocalTasks.Count; localTaskNum++)
                {
                    List<int> currentRemoteTasks = new List<int>();
                    currentRemoteTasks.Add(currentLocalTasks[localTaskNum]);

                    for (int taskNum = 0; taskNum < numOfTasks; taskNum++)
                    {
                        if (!currentLocalTasks.Contains(taskNum))
                        {
                            currentRemoteTasks.Add(taskNum);
                        }
                    }

                    remoteCommunicationEnergy += CalculateRemoteCommnucationEnergy(currentLocalTasks[localTaskNum], currentRemoteTasks, configuration.RemoteCommunicationInfo);
                }
            }

            communicationEnergy += localCommunicationEnergy + remoteCommunicationEnergy;

            return communicationEnergy;
        }

        private double CalculateLocalCommnucationEnergy(List<int> tasks, Communication communication)
        {
            double energy = 0;
            string[,] commnucationMap = communication.MapMatrix;

            for (int taskNumIndex = 0; taskNumIndex < tasks.Count - 1; taskNumIndex++)
            {
                int taskNum = tasks[taskNumIndex];
                for (int nextTaskNumIndex = taskNumIndex + 1; nextTaskNumIndex < tasks.Count; nextTaskNumIndex++)
                {
                    double currentEnergy = 0;
                    int nextTaskNum = tasks[nextTaskNumIndex];

                    currentEnergy += Convert.ToDouble(commnucationMap[taskNum, nextTaskNum]);
                    currentEnergy += Convert.ToDouble(commnucationMap[nextTaskNum, taskNum]);

                    energy += currentEnergy;
                }
            }

            return energy;
        }

        private double CalculateRemoteCommnucationEnergy(int baseTaskNum, List<int> tasks, Communication communication)
        {
            double energy = 0;
            string[,] commnucationMap = communication.MapMatrix;

            for (int taskNumIndex = 0; taskNumIndex < tasks.Count; taskNumIndex++)
            {
                int taskNum = tasks[taskNumIndex];
                double currentEnergy = 0;

                currentEnergy += Convert.ToDouble(commnucationMap[baseTaskNum, taskNum]);

                energy += currentEnergy;
            }

            return energy;
        }

        public List<ProcessorAllocation> CalculateProcessorAllocationValues(Allocation allocation, Configuration configuration)
        {
            List<ProcessorAllocation> processorAllocations = new List<ProcessorAllocation>();

            string[,] mapMatrix = allocation.MapMatrix;
            int numOfProcessors = mapMatrix.GetLength(0); // Number of rows
            int numOfTasks = mapMatrix.GetLength(1); // Number of columns
            string TASK_ON = "1";

            for (int row = 0; row < numOfProcessors; row++)
            {
                ProcessorAllocation processorAllocation = new ProcessorAllocation();
                processorAllocation.Allocation = "";

                for (int col = 0; col < numOfTasks; col++)
                {
                    string task = mapMatrix[row, col];

                    // Task distribution on each processor
                    if (col == numOfTasks - 1)
                    {
                        processorAllocation.Allocation += $"{task}";
                    }
                    else
                    {
                        processorAllocation.Allocation += $"{task},";
                    }

                    // Calculate RAM, Upload, Donwload
                    Task currentTask = configuration.Tasks[col];

                    if (task == TASK_ON)
                    {
                        processorAllocation.RAM = Math.Max(processorAllocation.RAM, currentTask.RAM);
                        processorAllocation.Upload = Math.Max(processorAllocation.Upload, currentTask.Upload);
                        processorAllocation.Download = Math.Max(processorAllocation.Download, currentTask.Download);
                    }
                }

                processorAllocations.Add(processorAllocation);
            }

            return processorAllocations;
        }
    }
}
