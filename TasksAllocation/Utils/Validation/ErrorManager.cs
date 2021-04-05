using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Validation
{
    class ErrorManager
    {
        public List<Error> Errors { get; set; }

        public ErrorManager()
        {
            // [[message, actual, expected]]
            Errors = new List<Error>();
        }
    }
}
