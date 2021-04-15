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
    class Configuration
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

        public bool Validate(string cffFilename, Validations validations)
        {
            if (cffFilename == null)
            {
                return false;
            }

            CffLogFile cffLogFile = new CffLogFile();
            CffLimits cffLimits = new CffLimits();
            CffProgram cffProgram = new CffProgram();
            CffTasks cffTasks = new CffTasks();
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
                    Tasks = cffTasks.ExtractTasks(line, Program.Tasks, validations);
                }

                lineNumber++;
            }

            streamReader.Close();

            /*foreach(Task task in Tasks)
            {
                Console.WriteLine(task);
            }*/

            // Check whether the LOGFILE section exists
            cffLogFile.LogFileSection.CheckValidPair(validations, cffFilename);

            // Check whether the LIMITS section exists
            cffLimits.LimitPairSection.CheckValidPair(validations, cffFilename);

            // Check whether the PROGRAM section exists
            cffProgram.ProgramPairSection.CheckValidPair(validations, cffFilename);

            // Check whether the log file has been assigned a value or not
            validations.CheckProcessedFileExists(LogFilename, EXPECTED_LOGFILE_FORMAT);

            // Check whether the Limits object has all valid property values
            cffLimits.ValidateLimitData(validations);

            // Check whether the Program object has all valid property values
            cffProgram.ValidateProgramData(validations);

            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }
    }
}
