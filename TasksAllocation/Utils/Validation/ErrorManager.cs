using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Validation
{
    class ErrorManager
    {
        public List<string[]> Errors { get; set; }

        public ErrorManager()
        {
            // [[message, error, expected]]
            Errors = new List<string[]>();
        }
    }
}
