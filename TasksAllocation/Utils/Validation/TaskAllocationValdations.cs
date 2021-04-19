using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.Validation
{
    class TaskAllocationValdations
    {
        public Validations ValidationsManager;

        public TaskAllocationValdations()
        {

        }

        public TaskAllocationValdations(Validations validations)
        {
            ValidationsManager = validations;
        }

        public bool IsEqual(string keyword, int taffValue, int cffValue)
        {
            if (taffValue != cffValue)
            {
                Error error = new Error();

                error.Message = $"The values of {keyword} in taff and cff file are not equal";
                error.ActualValue = $"{keyword} in taff is {taffValue}, and cff is {cffValue}";
                error.ExpectedValue = $"The {keyword} values in taff and cff file are the same";
                error.Filename = ValidationsManager.Filename;
                error.LineNumber = "";
                error.ErrorCode = ErrorCode.INVALID_ALLOCATION;

                ValidationsManager.ErrorValidationManager.Errors.Add(error);

                return false;
            }

            return true;
        }
    }
}
