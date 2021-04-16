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
    class CffProcessorType
    {
        public ProcessorType ProcessorTypeInfo { get; set; }
        public bool InsideProcessorType { get; set; }

        public CffProcessorType()
        {
            ProcessorTypeInfo = new ProcessorType();
            InsideProcessorType = false;
        }

        public void MarkInsideProcessorType(string line, Validations validations)
        {
            if (line == CffKeywords.OPENING_PROCESSOR_TYPE)
            {
                InsideProcessorType = true;
            }

            if (line == CffKeywords.CLOSING_PROCESSOR_TYPE)
            {
                // Check the required values are missing
                validations.CheckRequiredValueExist(ProcessorTypeInfo.Name, CffKeywords.PROCESSOR_TYPE_NAME);
                validations.CheckRequiredValueExist(ProcessorTypeInfo.C2.ToString(), CffKeywords.PROCESSOR_TYPE_C2);
                validations.CheckRequiredValueExist(ProcessorTypeInfo.C1.ToString(), CffKeywords.PROCESSOR_TYPE_C1);
                validations.CheckRequiredValueExist(ProcessorTypeInfo.C0.ToString(), CffKeywords.PROCESSOR_TYPE_C0);

                // Reset Processor Type data
                InsideProcessorType = false;
                ProcessorTypeInfo = new ProcessorType();
            }
        }

        public ProcessorType ExtractProcessType(string line, Validations validations)
        {
            if (InsideProcessorType)
            {
                ExtractProcessTypeName(line, validations);
                ExtractProcessTypeC2(line, validations);
                ExtractProcessTypeC1(line, validations);
                ExtractProcessTypeC0(line, validations);

                bool validProcessTypeInfo = CheckValidProcessorTypeInfo();

                if (validProcessTypeInfo)
                {
                    return ProcessorTypeInfo;
                }
            }

            return null;
        }

        private void ExtractProcessTypeName(string line, Validations validations)
        {
            if (ProcessorTypeInfo.Name == null && line.StartsWith(CffKeywords.PROCESSOR_TYPE_NAME))
            {
                ProcessorTypeInfo.Name = validations.ValidateStringPair(
                    line,
                    CffKeywords.PROCESSOR_TYPE_NAME);
            }
        }

        private void ExtractProcessTypeC2(string line, Validations validations)
        {
            if (ProcessorTypeInfo.C2 < 0 && line.StartsWith(CffKeywords.PROCESSOR_TYPE_C2))
            {
                ProcessorTypeInfo.C2 = validations.ValidateDoublePair(
                    line,
                    CffKeywords.PROCESSOR_TYPE_C2);
            }
        }

        private void ExtractProcessTypeC1(string line, Validations validations)
        {
            if (ProcessorTypeInfo.C1 < 0 && line.StartsWith(CffKeywords.PROCESSOR_TYPE_C1))
            {
                ProcessorTypeInfo.C1 = validations.ValidateDoublePair(
                    line,
                    CffKeywords.PROCESSOR_TYPE_C1);
            }
        }

        private void ExtractProcessTypeC0(string line, Validations validations)
        {
            if (ProcessorTypeInfo.C0 < 0 && line.StartsWith(CffKeywords.PROCESSOR_TYPE_C0))
            {
                ProcessorTypeInfo.C0 = validations.ValidateDoublePair(
                    line,
                    CffKeywords.PROCESSOR_TYPE_C0);
            }
        }

        private bool CheckValidProcessorTypeInfo()
        {
            return (
                ProcessorTypeInfo.Name != null &&
                ProcessorTypeInfo.C2 != -1 &&
                ProcessorTypeInfo.C1 != -1 &&
                ProcessorTypeInfo.C0 != -1);
        }
    }
}
