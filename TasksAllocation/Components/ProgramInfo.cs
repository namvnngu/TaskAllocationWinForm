using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class ProgramInfo
    {
        public double Duration { get; set; }
        public int Tasks { get; set; }
        public int Processors { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public ProgramInfo()
        {
            Duration = -1;
            Tasks = -1;
            Processors = -1;
        }

        public ProgramInfo(double duration, int tasks, int processors)
        {
            Duration = duration;
            Tasks = tasks;
            Processors = processors;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_PROGRAM, 
                CffKeywords.CLOSING_PROGRAM);
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine($"DURATION={Duration}");
            text.AppendLine($"TASKS={Tasks}");
            text.AppendLine($"PROCESSORS={Processors}");

            return text.ToString();
        }
    }
}
