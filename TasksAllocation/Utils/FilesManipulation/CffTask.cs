using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TasksAllocation.Components;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation.Utils.FilesManipulation
{
    class CffTask
    {
        public Task TaskInfo { get; set; }
        public bool InsideTask { get; set; }

        public CffTask()
        {
            TaskInfo = new Task();
            InsideTask = false;
        }

        public void MarkInsideTask(string line, Validations validations)
        {
            if (line == CffKeywords.OPENING_TASK)
            {
                InsideTask = true;
            }

            if (line == CffKeywords.CLOSING_TASK)
            {
                // Check the required values are missing
                validations.CheckRequiredValueExist(TaskInfo.ID.ToString(), CffKeywords.TASK_ID);
                validations.CheckRequiredValueExist(TaskInfo.Runtime.ToString(), CffKeywords.TASK_RAM);
                validations.CheckRequiredValueExist(TaskInfo.ReferenceFrequency.ToString(), CffKeywords.TASK_REFERENCE_FREQUENCY);
                validations.CheckRequiredValueExist(TaskInfo.RAM.ToString(), CffKeywords.TASK_RAM);
                validations.CheckRequiredValueExist(TaskInfo.Download.ToString(), CffKeywords.TASK_DOWNLOAD);
                validations.CheckRequiredValueExist(TaskInfo.Upload.ToString(), CffKeywords.TASK_UPLOAD);

                // Reset Task data
                InsideTask = false;
                TaskInfo = new Task();
            }
        }

        public Task ExtractTask(string line, Validations valadations)
        {
            if (InsideTask)
            {
                ExtractTaskID(line, valadations);
                ExtractTaskRuntime(line, valadations);
                ExtractTaskReferenceFrequency(line, valadations);
                ExtractTaskRAM(line, valadations);
                ExtractTaskDonwload(line, valadations);
                ExtractTaskUpload(line, valadations);

                bool validTaskInfo = CheckValidTaskInfo();

                if (validTaskInfo)
                {
                    return TaskInfo;
                }
            }

            return null;
        }

        private void ExtractTaskID(string line, Validations validations)
        {
            if (TaskInfo.ID < 0 && line.StartsWith(CffKeywords.TASK_ID))
            {
                TaskInfo.ID = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.TASK_ID);
            }
        }

        private void ExtractTaskRuntime(string line, Validations validations)
        {
            if (TaskInfo.Runtime < 0 && line.StartsWith(CffKeywords.TASK_RUNTIME))
            {
                TaskInfo.Runtime = validations.ValidateDoublePair(
                    line,
                    CffKeywords.TASK_RUNTIME);
            }
        } 

        private void ExtractTaskReferenceFrequency(string line, Validations validations)
        {
            if (TaskInfo.ReferenceFrequency < 0 && line.StartsWith(CffKeywords.TASK_REFERENCE_FREQUENCY))
            {
                TaskInfo.ReferenceFrequency = validations.ValidateDoublePair(
                    line,
                    CffKeywords.TASK_REFERENCE_FREQUENCY);
            }
        }

        private void ExtractTaskRAM(string line, Validations validations)
        {
            if (TaskInfo.RAM < 0 && line.StartsWith(CffKeywords.TASK_RAM))
            {
                TaskInfo.RAM = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.TASK_RAM);
            }
        }

        private void ExtractTaskDonwload(string line, Validations validations)
        {
            if (TaskInfo.Download < 0 && line.StartsWith(CffKeywords.TASK_DOWNLOAD))
            {
                TaskInfo.Download = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.TASK_DOWNLOAD);
            }
        }

        private void ExtractTaskUpload(string line, Validations validations)
        {
            if (TaskInfo.Upload < 0 && line.StartsWith(CffKeywords.TASK_UPLOAD))
            {
                TaskInfo.Upload = validations.ValidateIntegerPair(
                    line,
                    CffKeywords.TASK_UPLOAD);
            }
        }

        private bool CheckValidTaskInfo()
        {
            return (
                TaskInfo.ID != -1 &&
                TaskInfo.Runtime != -1 &&
                TaskInfo.ReferenceFrequency != -1 &&
                TaskInfo.RAM != -1 &&
                TaskInfo.Download != -1 &&
                TaskInfo.Upload != -1); 
        }
    }
}
