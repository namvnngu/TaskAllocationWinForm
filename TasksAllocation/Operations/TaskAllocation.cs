using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Operations
{
    class TaskAllocation
    {
        public string CffFilename { get; set; }
        List<string> Errors { get; set; }

        public bool GetCffFilename(string taffFilename)
        {
            CffFilename = null;
            Errors = new List<string>();

            return (Errors.Count == 0);
        }

        public bool Validate(string taffFilename)
        {
            Errors = new List<string>();

            return (Errors.Count == 0);
        }
    }
}
