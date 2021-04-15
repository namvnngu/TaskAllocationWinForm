using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Components;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation.Utils.FilesManipulation
{
    class CffProcessor
    {
        public Processor ProcessorInfo { get; set; }
        public bool InsideProcessor { get; set; }

        public CffProcessor()
        {
            ProcessorInfo = new Processor();
            InsideProcessor = false;
        }

        public void MarkInsideProcessor(string line, Validations validations)
        {
            if (line == CffKeywords.OPENING_PROCESSOR)
            {
                InsideProcessor = true;
            }

            if (line == CffKeywords.CLOSING_PROCESSOR)
            {
                // Check the required values are missing
                validations.CheckRequiredValueExist(ProcessorInfo.ID.ToString(), CffKeywords.PROCESSOR_ID);
                validations.CheckRequiredValueExist(ProcessorInfo.Type, CffKeywords.PROCESSOR_TYPE);
                validations.CheckRequiredValueExist(ProcessorInfo.Frequency.ToString(), CffKeywords.PROCESSOR_FREQUENCY);
                validations.CheckRequiredValueExist(ProcessorInfo.RAM.ToString(), CffKeywords.PROCESSOR_RAM);
                validations.CheckRequiredValueExist(ProcessorInfo.Download.ToString(), CffKeywords.PROCESSOR_DOWNLOAD);
                validations.CheckRequiredValueExist(ProcessorInfo.Upload.ToString(), CffKeywords.PROCESSOR_UPLOAD);

                // Reset Processor data
                InsideProcessor = false;
                ProcessorInfo = new Processor();
            }
        }

        public Processor ExtractProcessor(string line, Validations validations)
        {
            if (InsideProcessor)
            {
                ExtractProcessorID(line, validations);
                ExtractProcessorType(line, validations);
                ExtractProcessorFrequency(line, validations);
                ExtractProcessorRAM(line, validations);
                ExtractProcessorDownload(line, validations);
                ExtractProcessorUpload(line, validations);

                bool validProcessorInfo = CheckValidProcessorInfo();

                if (validProcessorInfo)
                {
                    return ProcessorInfo;
                }
            }

            return null;
        }

        private void ExtractProcessorID(string line, Validations validations)
        {
            if (ProcessorInfo.ID < 0 && line.StartsWith(CffKeywords.PROCESSOR_ID))
            {
                ProcessorInfo.ID = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.PROCESSOR_ID);
            }
        }

        private void ExtractProcessorType(string line, Validations validations)
        {
            if (ProcessorInfo.Type == null && line.StartsWith(CffKeywords.PROCESSOR_TYPE))
            {
                ProcessorInfo.Type = validations.ValidateStringPair(
                    line,
                    CffKeywords.PROCESSOR_TYPE);
            }
        }

        private void ExtractProcessorFrequency(string line, Validations validations)
        {
            if (ProcessorInfo.Frequency < 0 && line.StartsWith(CffKeywords.PROCESSOR_FREQUENCY))
            {
                ProcessorInfo.Frequency = validations.ValidateDoublePair(
                    line,
                    CffKeywords.PROCESSOR_FREQUENCY);
            }
        }

        private void ExtractProcessorRAM(string line, Validations validations)
        {
            if (ProcessorInfo.RAM < 0 && line.StartsWith(CffKeywords.PROCESSOR_RAM))
            {
                ProcessorInfo.RAM = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.PROCESSOR_RAM);
            }
        }

        private void ExtractProcessorDownload(string line, Validations validations)
        {
            if (ProcessorInfo.Download < 0 && line.StartsWith(CffKeywords.PROCESSOR_DOWNLOAD))
            {
                ProcessorInfo.Download = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.PROCESSOR_DOWNLOAD);
            }
        }

        private void ExtractProcessorUpload(string line, Validations validations)
        {
            if (ProcessorInfo.Upload < 0 && line.StartsWith(CffKeywords.PROCESSOR_UPLOAD))
            {
                ProcessorInfo.Upload = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.PROCESSOR_UPLOAD);
            }
        }

        private bool CheckValidProcessorInfo()
        {
            return (
                ProcessorInfo.ID != -1 &&
                ProcessorInfo.Type != null &&
                ProcessorInfo.Frequency != -1 &&
                ProcessorInfo.RAM != -1 &&
                ProcessorInfo.Download != -1 &&
                ProcessorInfo.Upload != -1);
        }
    }
}
