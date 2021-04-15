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
        public string Type { get; set; }
        public ProcessorType PType { get; set; }
        public double Frequency { get; set; }
        public int RAM { get; set; }
        public int Download { get; set; }
        public int Upload { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Processor()
        {
            ID = -1;
            Type = null;
            Frequency = -1;
            RAM = -1;
            Download = -1;
            Upload = -1;
        }

        public Processor(
            int id,
            string type,
            double frequency,
            int ram, 
            int download,
            int upload)
        {
            ID = id;
            Type = type;
            Frequency = frequency;
            RAM = ram;
            Download = download;
            Upload = upload;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_PROCESSOR, 
                CffKeywords.CLOSING_PROCESSOR);
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine($"PROCESSOR-{CffKeywords.PROCESSOR_ID}={ID}");
            text.AppendLine($"PROCESSOR-{CffKeywords.PROCESSOR_TYPE}={Type}");
            text.AppendLine($"PROCESSOR-{CffKeywords.PROCESSOR_FREQUENCY}={Frequency}");
            text.AppendLine($"PROCESSOR-{CffKeywords.PROCESSOR_RAM}={RAM}");
            text.AppendLine($"PROCESSOR-{CffKeywords.PROCESSOR_DOWNLOAD}={Download}");
            text.AppendLine($"PROCESSOR-{CffKeywords.PROCESSOR_UPLOAD}={Upload}");

            return text.ToString();
        }
    }
}
