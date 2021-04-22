using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TasksAllocation.Utils.Constants;

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

        public Dictionary<string, List<Error>> ClassifyFileError()
        {
            List<Error> cffErrors = new List<Error>();
            List<Error> taffErrors = new List<Error>();
            Dictionary<string, List<Error>> errorTypeDict = new Dictionary<string, List<Error>>();
            string cffExtension = $".{CffKeywords.FILE_EXTENSION}";
            string taffExtension = $".{TaffKeywords.FILE_EXTENSION}";

            for (int errorNum = 0; errorNum < Errors.Count; errorNum++)
            {
                Error error = Errors[errorNum];
                string fileExtension = Path.GetExtension(error.Filename);

                if (fileExtension == taffExtension)
                {
                    taffErrors.Add(error);
                } 
                else if (fileExtension == cffExtension)
                {
                    cffErrors.Add(error);
                }
            }

            errorTypeDict[TaffKeywords.FILE_EXTENSION] = taffErrors;
            errorTypeDict[CffKeywords.FILE_EXTENSION] = cffErrors;

            return errorTypeDict;
        }
    }
}
