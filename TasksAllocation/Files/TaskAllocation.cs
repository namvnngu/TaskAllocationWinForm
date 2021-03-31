using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TasksAllocation.Components;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.FilesManipulation;

namespace TasksAllocation.Files
{
    class TaskAllocation
    {
        public string CffFilename { get; set; }
        public int Count { get; set; }
        public int NumberOfTasks { get; set; }
        public int NumberOfProcessors { get; set; }
        public List<Allocation> Allocations { get; set; }

        public bool GetCffFilename(string taffFilename, ref ErrorManager errorManager)
        {
            int beforeNumOfError, afterNumOfError;
            beforeNumOfError = errorManager.Errors.Count;

            CffFilename = TaffManipulation.ExtractCff(taffFilename, ref errorManager);

            afterNumOfError = errorManager.Errors.Count;

            return (beforeNumOfError == afterNumOfError);
        }

        public bool Validate(string taffFilename, ref ErrorManager errorManager)
        {
            return (errorManager.Errors.Count == 0);
        }
    }
}
