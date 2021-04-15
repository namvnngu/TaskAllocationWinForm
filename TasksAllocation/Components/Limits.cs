using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class Limits
    {
        public int MinimumTasks { get; set; }
        public int MaximumTasks { get; set; }
        public int MinimumProcessors { get; set; }
        public int MaximumProcessors { get; set; }
        public double MinimumProcessorsFrequencies { get; set; }
        public double MaximumProcessorsFrequencies { get; set; }
        public int MinimumRAM { get; set; }
        public int MaximumRAM { get; set; }
        public int MinimumDownload { get; set; }
        public int MaximumDownload { get; set; }
        public int MinimumUpload { get; set; }
        public int MaximumUpload { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Limits()
        {
            MinimumTasks = -1;
            MaximumTasks = -1;
            MinimumProcessors = -1;
            MaximumProcessors = -1;
            MinimumProcessorsFrequencies = -1;
            MaximumProcessorsFrequencies = -1;
            MinimumRAM = -1;
            MaximumRAM = -1;
            MinimumDownload = -1;
            MaximumDownload = -1;
            MinimumUpload = -1;
            MaximumUpload = -1;
        }

        public Limits(
            int minimumTasks,
            int maxmumTasks,
            int minimumProcessors,
            int maximumProcessors,
            double minimumProcessorsFrequencies,
            double maximumProcessorsFrequencies,
            int minimumRAM,
            int maximumRAM,
            int minimumDownload,
            int maximumDownload,
            int minimumUpload,
            int maximumUpload)
        {
            MinimumTasks = minimumTasks;
            MaximumTasks = maxmumTasks;
            MinimumProcessors = minimumProcessors;
            MaximumProcessors = maximumProcessors;
            MinimumProcessorsFrequencies = minimumProcessorsFrequencies;
            MaximumProcessorsFrequencies = maximumProcessorsFrequencies;
            MinimumRAM = minimumRAM;
            MaximumRAM = maximumRAM;
            MinimumDownload = minimumDownload;
            MaximumDownload = maximumDownload;
            MinimumUpload = minimumUpload;
            MaximumUpload = maximumUpload;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_LIMITS, 
                CffKeywords.CLOSING_LIMITS);
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine($"{CffKeywords.MINIMUM_TASKS}={MinimumTasks}");
            text.AppendLine($"{CffKeywords.MAXIMUM_TASKS}={MaximumTasks}");
            text.AppendLine($"{CffKeywords.MINIMUM_PROCESSORS}={MinimumProcessors}");
            text.AppendLine($"{CffKeywords.MAXIMUM_PROCESSORS}={MaximumProcessors}");
            text.AppendLine($"{CffKeywords.MINIMUM_PROCESSOR_FREQUENCIES}={MinimumProcessorsFrequencies}");
            text.AppendLine($"{CffKeywords.MAXIMUM_PROCESSOR_FREQUENCIES}={MaximumProcessorsFrequencies}");
            text.AppendLine($"{CffKeywords.MINIMUM_RAM}={MinimumRAM}");
            text.AppendLine($"{CffKeywords.MAXIMUM_RAM}={MaximumRAM}");
            text.AppendLine($"{CffKeywords.MINIMUM_DOWNLOAD}={MinimumDownload}");
            text.AppendLine($"{CffKeywords.MAXINUM_DOWNLOAD}={MaximumDownload}");
            text.AppendLine($"{CffKeywords.MINIMUM_UPLOAD}={MinimumUpload}");
            text.AppendLine($"{CffKeywords.MAXIMUM_UPLOAD}={MaximumUpload}");

            return text.ToString(); 
        }
    }
}
