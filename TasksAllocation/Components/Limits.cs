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

            text.AppendLine($"MINIMUM-TASKS={MinimumTasks}");
            text.AppendLine($"MAXIMUM-TASKS={MaximumTasks}");
            text.AppendLine($"MINIMUM-PROCESSORS={MinimumProcessors}");
            text.AppendLine($"MAXIMUM-PROCESSORS={MaximumProcessors}");
            text.AppendLine($"MINIMUM-PROCESSOR-FREQUENCIES={MinimumProcessorsFrequencies}");
            text.AppendLine($"MAXIMUM-PROCESSOR-FREQUENCIES={MaximumProcessorsFrequencies}");
            text.AppendLine($"MINIMUM-RAM={MinimumRAM}");
            text.AppendLine($"MAXIMUM-RAM={MaximumRAM}");
            text.AppendLine($"MINIMUM-DOWNLOAD={MinimumDownload}");
            text.AppendLine($"MAXIMUM-DOWNLOAD={MaximumDownload}");
            text.AppendLine($"MINIMUM-UPLOAD={MinimumUpload}");
            text.AppendLine($"MAXIMUM-UPLOAD={MaximumUpload}");

            return text.ToString(); 
        }
    }
}
