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
    class CffLimits
    {
        public Limits LimitData { get; set; }
        public PairSection LimitPairSection { get; set; }

        public CffLimits()
        {
            LimitData = new Limits();
            LimitPairSection = new PairSection(
                CffKeywords.OPENING_LIMITS,
                CffKeywords.CLOSING_LIMITS);
        }

        public Limits ExtractLimitData(string line, Validations validations)
        {
            // Check whether the line starts opening/closing LIMITS section
            // If yes, mark it exist
            LimitPairSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            ExtractMinimumTasks(line, validations);
            ExtractMaximumTasks(line, validations);
            ExtractMinimumProcessors(line, validations);
            ExtractMaximumProcessors(line, validations);
            ExtractMinimumProcessorFrequencies(line, validations);
            ExtractMaximumProcessorFrequencies(line, validations);
            ExtractMinimumRAM(line, validations);
            ExtractMaximumRAM(line, validations);
            ExtractMinimumDownload(line, validations);
            ExtractMaximumDownload(line, validations);
            ExtractMinimumUpload(line, validations);
            ExtractMaximumUpload(line, validations);

            return LimitData;
        }

        public void ExtractMinimumTasks(string line, Validations validations)
        {
            if (LimitData.MinimumTasks < 0 && line.StartsWith(CffKeywords.MINIMUM_TASKS))
            {
                LimitData.MinimumTasks = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MINIMUM_TASKS);
            }
        }

        public void ExtractMaximumTasks(string line, Validations validations)
        {
            if (LimitData.MaximumTasks < 0 && line.StartsWith(CffKeywords.MAXIMUM_TASKS))
            {
                LimitData.MaximumTasks = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MAXIMUM_TASKS);
            }
        }

        public void ExtractMinimumProcessors(string line, Validations validations)
        {
            if (LimitData.MinimumProcessors < 0 && line.StartsWith(CffKeywords.MINIMUM_PROCESSORS))
            {
                LimitData.MinimumProcessors = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MINIMUM_PROCESSORS);
            }
        }

        public void ExtractMaximumProcessors(string line, Validations validations)
        {
            if (LimitData.MaximumProcessors < 0 && line.StartsWith(CffKeywords.MAXIMUM_PROCESSORS))
            {
                LimitData.MaximumProcessors = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MAXIMUM_PROCESSORS);
            }
        }

        public void ExtractMinimumProcessorFrequencies(string line, Validations validations)
        {
            if (LimitData.MinimumProcessorsFrequencies < 0 && line.StartsWith(CffKeywords.MINIMUM_PROCESSOR_FREQUENCIES))
            {
                LimitData.MinimumProcessorsFrequencies = validations.ValidateDoublePair(
                    line,
                    CffKeywords.MINIMUM_PROCESSOR_FREQUENCIES);
            }
        }

        public void ExtractMaximumProcessorFrequencies(string line, Validations validations)
        {
            if (LimitData.MaximumProcessorsFrequencies < 0 && line.StartsWith(CffKeywords.MAXIMUM_PROCESSOR_FREQUENCIES))
            {
                LimitData.MaximumProcessorsFrequencies = validations.ValidateDoublePair(
                    line,
                    CffKeywords.MAXIMUM_PROCESSOR_FREQUENCIES);
            }
        }

        public void ExtractMinimumRAM(string line, Validations validations)
        {
            if (LimitData.MinimumRAM < 0 && line.StartsWith(CffKeywords.MINIMUM_RAM))
            {
                LimitData.MinimumRAM = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MINIMUM_RAM);
            }
        }

        public void ExtractMaximumRAM(string line, Validations validations)
        {
            if (LimitData.MaximumRAM < 0 && line.StartsWith(CffKeywords.MAXIMUM_RAM))
            {
                LimitData.MaximumRAM = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MAXIMUM_RAM);
            }
        }

        public void ExtractMinimumDownload(string line, Validations validations)
        {
            if (LimitData.MinimumDownload < 0 && line.StartsWith(CffKeywords.MINIMUM_DOWNLOAD))
            {
                LimitData.MinimumDownload = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MINIMUM_DOWNLOAD);
            }
        }

        public void ExtractMaximumDownload(string line, Validations validations)
        {
            if (LimitData.MaximumDownload < 0 && line.StartsWith(CffKeywords.MAXINUM_DOWNLOAD))
            {
                LimitData.MaximumDownload = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MAXINUM_DOWNLOAD);
            }
        }

        public void ExtractMinimumUpload(string line, Validations validations)
        {
            if (LimitData.MinimumUpload < 0 && line.StartsWith(CffKeywords.MINIMUM_UPLOAD))
            {
                LimitData.MinimumUpload = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MINIMUM_UPLOAD);
            }
        }

        public void ExtractMaximumUpload(string line, Validations validations)
        {
            if (LimitData.MaximumUpload < 0 && line.StartsWith(CffKeywords.MAXIMUM_UPLOAD))
            {
                LimitData.MaximumUpload = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.MAXIMUM_UPLOAD);
            }
        }
    }
}
