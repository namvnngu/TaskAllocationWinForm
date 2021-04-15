using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Components;

namespace TasksAllocation.Utils.FilesManipulation
{
    class CffTasks
    {
        public PairSection TasksSection { get; set; }
        public List<Task>  Tasks { get; set; }
        public KeywordPair TaskPair { get; set; }
        public CffTask CffTaskExtraction { get; set; }

        public CffTasks()
        {
            TasksSection = new PairSection(
                CffKeywords.OPENING_TASKS,
                CffKeywords.CLOSING_TASKS);
            Tasks = new List<Task>();
            TaskPair = new KeywordPair(
                CffKeywords.OPENING_TASK,
                CffKeywords.CLOSING_TASK);
            CffTaskExtraction = new CffTask();
        }

        public List<Task> ExtractTasks(string line, Validations validations)
        {
            // Check whether the line starts opening/closing TASKS section
            // If yes, mark it exist
            TasksSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            // Count TASK section
            TaskPair.CheckValidKeyword(line);

            // Extract Task data within TASK section
            if (TasksSection.ValidSectionPair[0] &&
                !TasksSection.ValidSectionPair[1])
            {
                Task task;

                // Check whether the reader goes within the TASK section
                CffTaskExtraction.MarkInsideTask(line, validations);

                task = CffTaskExtraction.ExtractTask(line, validations);
                if (task != null)
                {
                    Tasks.Add(task);
                }
            }

            return Tasks;
        }
    }
}
