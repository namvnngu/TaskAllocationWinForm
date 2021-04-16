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
    class CffProcessorTypes
    {
        public PairSection ProcessorTypesSection { get; set; }
        public List<ProcessorType> ProcessorTypes { get; set; }
        public KeywordPair ProcessorTypePair { get; set; }
        public CffProcessorType CffProcessorTypeExtraction { get; set; }

        public CffProcessorTypes()
        {
            ProcessorTypesSection = new PairSection(
                CffKeywords.OPENING_PROCESSOR_TYPES,
                CffKeywords.CLOSING_PROCESSOR_TYPES);
            ProcessorTypes = new List<ProcessorType>();
            ProcessorTypePair = new KeywordPair(
                CffKeywords.OPENING_PROCESSOR_TYPE,
                CffKeywords.CLOSING_PROCESSOR_TYPE);
            CffProcessorTypeExtraction = new CffProcessorType();
        }

        public List<ProcessorType> ExtractProcessorTypes(string line, Validations validations)
        {
            // Check whether the line starts opening/closing PROCESSOR-TYPES section
            // If yes, mark it exist
            ProcessorTypesSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            // Count PROCESSOR-TYPE section
            ProcessorTypePair.CheckValidKeyword(line);

            // Extract Processor Type data within PROCESSOR-TYPE section
            if (ProcessorTypesSection.ValidSectionPair[0] &&
                !ProcessorTypesSection.ValidSectionPair[1])
            {
                ProcessorType processorType;

                // Check whether the reader goes within the PROCESSOR-TYPE section
                CffProcessorTypeExtraction.MarkInsideProcessorType(line, validations);

                processorType = CffProcessorTypeExtraction.ExtractProcessType(line, validations);

                if (processorType != null)
                {
                    ProcessorTypes.Add(processorType);
                }
            }

            return ProcessorTypes;
        }
    }
}
