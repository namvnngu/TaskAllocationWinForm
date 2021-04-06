using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Validation
{
    class Error
    {
        public string Message { get; set; }
        public string ActualValue { get; set; }
        public string ExpectedValue { get; set; }
        public string Filename { get; set; }
        public string LineNumber { get; set; }
        public int ErrorCode { get; set; }

        public Error()
        {

        }

        public Error(string message, string actualValue, string expectedValue)
        {
            Message = message;
            ActualValue = actualValue;
            ExpectedValue = expectedValue;
            Filename = "";
            LineNumber = "";
        }

        public Error(
            string message, 
            string actualValue, 
            string expectedValue, 
            string fileName)
        {
            Message = message;
            ActualValue = actualValue;
            ExpectedValue = expectedValue;
            Filename = fileName;
            LineNumber = "";
        }

        public Error(
            string message, 
            string actualValue, 
            string expectedValue, 
            string filename, 
            string lineNumber)
        {
            Message = message;
            ActualValue = actualValue;
            ExpectedValue = expectedValue;
            Filename = filename;
            LineNumber = lineNumber;
        }

        public Error(
            string message, 
            string actualValue, 
            string expectedValue, 
            string filename, 
            string lineNumber, 
            int errorCode)
        {
            Message = message;
            ActualValue = actualValue;
            ExpectedValue = expectedValue;
            Filename = filename;
            LineNumber = lineNumber;
            ErrorCode = errorCode;
        }
    }
}
