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
        public int MinimumProcessorsFrequencies { get; set; }
        public int MaximumProcessorsFrequencies { get; set; }
        public int MinimumRAM { get; set; }
        public int MaximumRAM { get; set; }
        public int MinimumDownload { get; set; }
        public int MaximumDownload { get; set; }
        public int MinimumUpload { get; set; }
        public int MaximumUpload { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Limits(
            int minimumTasks,
            int maxmumTasks,
            int minimumProcessors,
            int maximumProcessors,
            int minimumProcessorsFrequencies,
            int maximumProcessorsFrequencies,
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
    }
}
