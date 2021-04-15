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
        public double Runtime { get; set; }
        public double ReferenceFrequency { get; set; }
        public int RAM { get; set; }
        public int Download { get; set; }
        public int Upload { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Task()
        {
            ID = -1;
            Runtime = -1;
            ReferenceFrequency = -1;
            RAM = -1;
            Download = -1;
            Upload = -1;
        }

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
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_TASK, 
                CffKeywords.CLOSING_TASK);
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine($"TASK-{CffKeywords.TASK_ID}={ID}");
            text.AppendLine($"TASK-{CffKeywords.TASK_RUNTIME}={Runtime}");
            text.AppendLine($"TASK-{CffKeywords.TASK_REFERENCE_FREQUENCY}={ReferenceFrequency}");
            text.AppendLine($"TASK-{CffKeywords.TASK_RAM}={RAM}");
            text.AppendLine($"TASK-{CffKeywords.TASK_DOWNLOAD}={Download}");
            text.AppendLine($"TASK-{CffKeywords.TASK_UPLOAD}={Upload}");

            return text.ToString();
        }
    }
}
