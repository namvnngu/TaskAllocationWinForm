using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class Program
    {
        public int Duration { get; set; }
        public int Tasks { get; set; }
        public int Processors { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Program(int duration, int tasks, int processors)
        {
            Duration = duration;
            Tasks = tasks;
            Processors = processors;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_PROGRAM, 
                CffKeywords.CLOSING_PROGRAM);
        }
    }
}
