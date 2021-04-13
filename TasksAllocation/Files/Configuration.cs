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

        public bool Validate(string cffFilename, Validations validations)
        {
            if (cffFilename == null)
            {
                return false;
            }

            CffLogFile cffLogFile = new CffLogFile();
            int beforeNumOfError, afterNumOfError;
            int lineNumber = 1;
            string line;
            StreamReader streamReader = new StreamReader(cffFilename);

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
                

                // If the LOGFILE section is already visited, ignore
                if (!cffLogFile.LogFileSection.ValidSectionPair[1])
                {
                    LogFilename = cffLogFile.ExtractLogFile(cffFilename, line, validations);
                }
                Console.WriteLine(LogFilename);


                lineNumber++;
            }


            afterNumOfError = validations.ErrorValidationManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }
    }
}
