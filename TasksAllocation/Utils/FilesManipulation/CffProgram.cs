using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Components;

namespace TasksAllocation.Utils.FilesManipulation
{
    class CffProgram
    {
        public ProgramInfo Program { get; set; }
        public PairSection ProgramPairSection { get; set; }

        public CffProgram()
        {
            Program = new ProgramInfo();
            ProgramPairSection = new PairSection(
                CffKeywords.OPENING_PROGRAM,
                CffKeywords.CLOSING_PROGRAM);
        }

        public ProgramInfo ExtractProgramData(string line, Validations validations)
        {
            // Check whether the line starts opening/closing PROGRAM section
            // If yes, mark it exist
            ProgramPairSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            bool openingSectionVisited = ProgramPairSection.ValidSectionPair[0];

            if (openingSectionVisited)
            {
                ExtractDuration(line, validations);
                ExtractTasks(line, validations);
                ExtractProcessors(line, validations);
            }

            return Program;
        }

        public void ExtractDuration(string line, Validations validations)
        {
            if (Program.Duration < 0 && line.StartsWith(CffKeywords.PROGRAM_DURATION))
            {
                Program.Duration = validations.ValidateDoublePair(
                    line,
                    CffKeywords.PROGRAM_DURATION);
            }
        }

        public void ExtractTasks(string line, Validations validations)
        {
            if (Program.Tasks < 0 && line.StartsWith(CffKeywords.PROGRAM_TASKS))
            {
                Program.Tasks = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.PROGRAM_TASKS);
            }
        }

        public void ExtractProcessors(string line, Validations validations)
        {
            if (Program.Processors < 0 && line.StartsWith(CffKeywords.PROGRAM_PROCESSORS))
            {
                Program.Processors = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.PROGRAM_PROCESSORS);
            }
        }

        public void ValidateProgramData(Validations validations)
        {
            if (Program.Duration == -1)
            {
                CreateError(CffKeywords.PROGRAM_DURATION, validations);
            }

            if (Program.Tasks == -1)
            {
                CreateError(CffKeywords.PROGRAM_TASKS, validations);
            }

            if (Program.Processors == -1)
            {
                CreateError(CffKeywords.PROGRAM_PROCESSORS, validations);
            }
        }

        public void CreateError(string keyword, Validations validations)
        {
            string message = $"{keyword} value is missing";
            string actualValue = "null";
            string expectedValue = "An integer (or a floating number for processor frequencies)";
            string noLinenumber = "";

            Error error = new Error(
                message,
                actualValue,
                expectedValue,
                validations.Filename,
                noLinenumber,
                ErrorCode.MISSING_VALUE);

            validations.ErrorValidationManager.Errors.Add(error);
        }
    }
}
