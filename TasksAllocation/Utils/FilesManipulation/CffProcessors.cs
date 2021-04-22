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
    class CffProcessors
    {
        public PairSection ProcessorsSection { get; set; }
        public List<Processor> Processors { get; set; }
        public KeywordPair ProcessorPair { get; set; }
        public CffProcessor CffProcessorkExtraction { get; set; }

        public CffProcessors()
        {
            ProcessorsSection = new PairSection(
                CffKeywords.OPENING_PROCESSORS,
                CffKeywords.CLOSING_PROCESSORS);
            Processors = new List<Processor>();
            ProcessorPair = new KeywordPair(
                CffKeywords.OPENING_PROCESSOR,
                CffKeywords.CLOSING_PROCESSOR);
            CffProcessorkExtraction = new CffProcessor();
        }

        public List<Processor> ExtractProcessors(string line, Validations validations)
        {
            // Check whether the line starts opening/closing PROCESSORS section
            // If yes, mark it exist
            ProcessorsSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            // Count PROCESSOR section
            ProcessorPair.CheckValidKeyword(line);

            // Extract Processor data within PROCESSOR section
            if (ProcessorsSection.ValidSectionPair[0] &&
                !ProcessorsSection.ValidSectionPair[1])
            {
                Processor processor;

                // Check whether the reader goes within the PROCESSOR section
                CffProcessorkExtraction.MarkInsideProcessor(line, validations);

                processor = CffProcessorkExtraction.ExtractProcessor(line, validations);
                if (processor != null)
                {
                    Processors.Add(processor);
                }
            }
            
            return Processors;
        }
    }
}
