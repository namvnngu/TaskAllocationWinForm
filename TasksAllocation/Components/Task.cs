using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class Task
    {
        public int ID { get; set; }
        public int Runtime { get; set; }
        public int ReferenceFrequency { get; set; }
        public int RAM { get; set; }
        public int Download { get; set; }
        public int Upload { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Task(
            int id,
            int runtime,
            int referenceFrequency,
            int ram,
            int download,
            int upload)
        {
            ID = id;
            Runtime = runtime;
            ReferenceFrequency = referenceFrequency;
            RAM = ram;
            Download = download;
            Upload = upload;
            OpeningClosingSection = new PairSection(CffKeywords.OPENING_TASK, CffKeywords.CLOSING_TASK);
        }
    }
}
