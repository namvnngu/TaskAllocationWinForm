using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class Processor
    {
        public int ID { set; get; }
        public ProcessorType PType { get; set; }
        public int Frequency { get; set; }
        public int RAM { get; set; }
        public int Download { get; set; }
        public int Upload { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Processor(
            int id,
            ProcessorType pType, 
            int frequency,
            int ram, 
            int download,
            int upload)
        {
            ID = id;
            PType = pType;
            Frequency = frequency;
            RAM = ram;
            Download = download;
            Upload = upload;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_PROCESSOR, 
                CffKeywords.CLOSING_PROCESSOR);
        }
    }
}
