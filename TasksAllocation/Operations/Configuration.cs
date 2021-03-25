using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Operations
{
    class Configuration
    {
        public string LogFilename { get; set; }
        List<string> Errors { get; set; }

        public bool Validate(string cffFilename)
        {
            Errors = new List<string>();

            return (Errors.Count == 0);
        }
    }
}
