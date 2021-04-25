using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TasksAllocation.Components;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.FilesManipulation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Files
{
    /// <summary>
    /// The class valiates the provided CFF file, extracts all 
    /// necessary values and scan errors while reading the file.
    /// </summary>
    public class Configuration
    {
        public string LogFilename { get; set; }
        public Limits LimitData { get; set; }
        public ProgramInfo Program { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Processor> Processors { get; set; }
        public List<ProcessorType> ProcessorTypes { get; set; }
        public LocalCommunication LocalCommunicationInfo { get; set; }
        public RemoteCommunication RemoteCommunicationInfo { get; set; }

        public Configuration()
        {
            LogFilename = null;
            LimitData = null;
        }

        public bool ValidateFile(string cffFilename, Validations validations)
        {
            if (cffFilename == null)
            {
                return false;
            }

            CffLogFile cffLogFile = new CffLogFile();
            CffLimits cffLimits = new CffLimits();
            CffProgram cffProgram = new CffProgram();
            CffTasks cffTasks = new CffTasks();
            CffProcessors cffProcessors = new CffProcessors();
            CffProcessorTypes cffProcessorTypes = new CffProcessorTypes();
            CffLocalCommunication cffLocalCommunication = new CffLocalCommunication();
            CffRemoteCommunication cffRemoteCommunication = new CffRemoteCommunication();
            int beforeNumOfError, afterNumOfError;
            int lineNumber = 1;
            string line;
            StreamReader streamReader = new StreamReader(cffFilename);
            string EXPECTED_LOGFILE_FORMAT = $"\"[name].{cffLogFile.LOGFILE_EXTENSION}\"";

            validations.Filename = cffFilename;
            beforeNumOfError = validations.ErrorValidationManager.Errors.Count;

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();
                validations.LineNumber = lineNumber.ToString();

                // Check if keyword is valid or not
                validations.CheckValidKeyword(line, CffKeywords.KEYWORD_DICT);

                // Extract and validate the LOGFILE section
                // If the LOGFILE section is already visited, then ignore
                if (!cffLogFile.LogFileSection.ValidSectionPair[1])
                {
                    LogFilename = cffLogFile.ExtractLogFile(cffFilename, line, validations);
                }

                // Extract and validate the LIMITS section
                // If the LIMITS sections is already visited, then ignore
                if (!cffLimits.LimitPairSection.ValidSectionPair[1])
                {
                    LimitData = cffLimits.ExtractLimitData(line, validations);
                }

                // Extract and validate the PROGRAM section
                // If the LIMITS sections is already visited, then ignore
                if (!cffProgram.ProgramPairSection.ValidSectionPair[1])
                {
                    Program = cffProgram.ExtractProgramData(line, validations);
                }

                // Extract and validate the TASKS section
                // If the TASKS sections is already visited, then ignore
                if (!cffTasks.TasksSection.ValidSectionPair[1])
                {
                    Tasks = cffTasks.ExtractTasks(line, validations);
                }

                // Extract and validate the PROCESSORS section
                // If the PROCESSORS sections is already visited, then ignore
                if (!cffProcessors.ProcessorsSection.ValidSectionPair[1])
                {
                    Processors = cffProcessors.ExtractProcessors(line, validations);
                }

                // Extract and validate the PROCESSOR-TYPES section
                // If the PROCESSOR-TYPES sections is already visited, then ignore
                if (!cffProcessorTypes.ProcessorTypesSection.ValidSectionPair[1])
                {
                    ProcessorTypes = cffProcessorTypes.ExtractProcessorTypes(line, validations);
                }

                // Extract and validate the LOCAL-COMMUNICATION section
                // If the LOCAL-COMMUNICATION sections is already visited, then ignore
                if (!cffLocalCommunication.LocalCommunicationSection.ValidSectionPair[1])
                {
                    LocalCommunicationInfo = cffLocalCommunication.ExtractLocalCommunication(line, Program.Tasks, validations);
                }

                // Extract and validate the REMOTE-COMMUNICATION section
                // If the REMOTE-COMMUNICATION sections is already visited, then ignore
                if (!cffRemoteCommunication.RemoteCommunicationSection.ValidSectionPair[1])
                {
                    RemoteCommunicationInfo = cffRemoteCommunication.ExtractRemoteCommunication(line, Program.Tasks, validations);
                }

                lineNumber++;
            }

            streamReader.Close();

            // Assign the corresponding Processor Type object to Processor
            AssignProcessType();

            // Console.WriteLine(RemoteCommunicationInfo);

            // Check whether the LOGFILE section exists
            cffLogFile.LogFileSection.CheckValidPair(validations, cffFilename);

            // Check whether the LIMITS section exists
            cffLimits.LimitPairSection.CheckValidPair(validations, cffFilename);

            // Check whether the PROGRAM section exists
            cffProgram.ProgramPairSection.CheckValidPair(validations, cffFilename);

            // Check whether the TASKS section exists
            cffTasks.TasksSection.CheckValidPair(validations, cffFilename);

            // Check whether the PROCCESORS section exists
            cffProcessors.ProcessorsSection.CheckValidPair(validations, cffFilename);

            // Check whether the PROCESSOR-TYPES section exists
            cffProcessorTypes.ProcessorTypesSection.CheckValidPair(validations, cffFilename);

            // Check whether the LOCAL-COMMUNICATION section exists
            cffLocalCommunication.LocalCommunicationSection.CheckValidPair(validations, cffFilename);

            // Check whether the REMOTE-COMMUNICATION section exists
            cffRemoteCommunication.RemoteCommunicationSection.CheckValidPair(validations, cffFilename);

            // Check whether the log file has been assigned a value or not
            validations.CheckProcessedFileExists(LogFilename, EXPECTED_LOGFILE_FORMAT);

            // Check whether the Limits object has all valid property values
            cffLimits.ValidateLimitData(validations);

            // Check whether the Program object has all valid property values
            cffProgram.ValidateProgramData(validations);

            // Check whether the number of tasks extracted is equal to the required number
            validations.CheckValidQuantity(
                cffTasks.TaskPair.CalculateNumOfPair().ToString(),
                cffProgram.Program.Tasks.ToString(),
                CffKeywords.OPENING_TASKS,
                ErrorCode.MISSING_SECTION);

            // Check whether the number of processors extracted is equal to the required number
            validations.CheckValidQuantity(
                cffProcessors.ProcessorPair.CalculateNumOfPair().ToString(),
                cffProgram.Program.Processors.ToString(),
                CffKeywords.OPENING_PROCESSORS,
                ErrorCode.MISSING_SECTION);

            // Check whether the processor type of each processor is missing
            CheckMissingProcessorType(validations);

            // Check whether the local communication is missing
            cffLocalCommunication.ValidateLocalCommunication(validations);

            // Check whether the remote communication is missing
            cffRemoteCommunication.ValidateRemoteCommunication(validations);

            // Configuration values must be within their corresponding limit
            ValidateConfigurationValues(validations);

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }

        /// <summary>
        /// The method assigns process types to appropriate,
        /// corresponding processors.
        /// </summary>
        private void AssignProcessType()
        {
            if (ProcessorTypes.Count != 0 && Processors.Count != 0)
            {
                foreach (Processor processor in Processors)
                {
                    foreach (ProcessorType processorType in ProcessorTypes)
                    {
                        if (processor.Type == processorType.Name)
                        {
                            processor.PType = processorType;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The method collects errors if the process type is missing.
        /// </summary>
        /// <param name="validations"></param>
        private void CheckMissingProcessorType(Validations validations)
        {
            foreach (Processor processor in Processors)
            {
                if (processor.PType == null)
                {
                    Error error = new Error();

                    error.Message = $"The processor type {processor.Type} of " +
                        $"the processor (ID={processor.ID}) does not exist";
                    error.ActualValue = "null";
                    error.ExpectedValue = $"The processor type should be listed in" +
                        $" the {CffKeywords.OPENING_PROCESSOR_TYPES} list in the CONFIGURATION file";
                    error.Filename = validations.Filename;
                    error.LineNumber = "";
                    error.ErrorCode = ErrorCode.MISSING_VALUE;

                    validations.ErrorValidationManager.Errors.Add(error);
                }
            }
        }

        /// <summary>
        /// The method supports to create an error when it occurs.
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="actualValue"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="validations"></param>
        private void CreateError(string keyword, string actualValue, string minValue, string maxValue, Validations validations)
        {
            Error error = new Error();

            error.Message = $"The value of {keyword} is out of limit";
            error.ActualValue = $"{actualValue}";
            error.ExpectedValue = $"The value of {keyword} must be in range between {minValue} and {maxValue} inclusively";
            error.Filename = validations.Filename;
            error.ErrorCode = ErrorCode.OUT_OF_LIMIT;

            validations.ErrorValidationManager.Errors.Add(error);
        }

        private void ValidateConfigurationValues(Validations validations)
        {
            if (Program.Tasks < LimitData.MinimumTasks ||
                Program.Tasks > LimitData.MaximumTasks)
            {
                CreateError(
                    "tasks", 
                    Program.Tasks.ToString(), 
                    LimitData.MinimumTasks.ToString(), 
                    LimitData.MaximumTasks.ToString(),
                    validations);
            }

            if (Program.Processors < LimitData.MinimumProcessors ||
                Program.Processors > LimitData.MaximumProcessors)
            {
                CreateError(
                    "processors", 
                    Program.Processors.ToString(), 
                    LimitData.MinimumProcessors.ToString(),
                    LimitData.MaximumProcessors.ToString(),
                    validations);
            }

            foreach(Processor processor in Processors)
            {
                if (processor.Frequency < LimitData.MinimumProcessorsFrequencies ||
                    processor.Frequency > LimitData.MaximumProcessorsFrequencies)
                {
                    CreateError(
                        $"process's frequency (ID={processor.ID})",
                        processor.Frequency.ToString(),
                        LimitData.MinimumProcessorsFrequencies.ToString(),
                        LimitData.MaximumProcessorsFrequencies.ToString(),
                        validations);
                }

                if (processor.RAM < LimitData.MinimumRAM ||
                    processor.RAM > LimitData.MaximumRAM)
                {
                    CreateError(
                        $"process's RAM (ID={processor.ID})",
                        processor.RAM.ToString(),
                        LimitData.MinimumRAM.ToString(),
                        LimitData.MaximumRAM.ToString(),
                        validations);
                }

                if (processor.Download < LimitData.MinimumDownload ||
                    processor.Download > LimitData.MaximumDownload)
                { 
                    CreateError(
                        $"process's download (ID={processor.ID})",
                        processor.Download.ToString(),
                        LimitData.MinimumDownload.ToString(),
                        LimitData.MaximumDownload.ToString(),
                        validations);
                }

                if (processor.Upload < LimitData.MinimumUpload ||
                    processor.Upload > LimitData.MaximumUpload)
                {
                    CreateError(
                        $"process's upload (ID={processor.ID})",
                        processor.Upload.ToString(),
                        LimitData.MinimumUpload.ToString(),
                        LimitData.MaximumUpload.ToString(),
                        validations);
                }
            }
        }
    }
}
