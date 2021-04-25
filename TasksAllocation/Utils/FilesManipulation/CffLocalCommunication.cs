using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Components;
using TasksAllocation.Utils.DataStructure;

namespace TasksAllocation.Utils.FilesManipulation
{
    public class CffLocalCommunication
    {
        public LocalCommunication LocalCommunication { get; set; }
        public PairSection LocalCommunicationSection { get; set; }
        
        public CffLocalCommunication()
        {
            LocalCommunication = new LocalCommunication();
            LocalCommunicationSection = new PairSection(
                CffKeywords.OPENING_LOCAL_COMMUNICATION,
                CffKeywords.CLOSING_LOCAL_COMMUNICATION);
        }

        public LocalCommunication ExtractLocalCommunication(string line, int numOfTasks, Validations validations)
        {
            // Check whether the line starts opening/closing LOCAL-COMMUNICATION section
            // If yes, mark it exist
            LocalCommunicationSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            bool openingSectionVisited = LocalCommunicationSection.ValidSectionPair[0];

            if (openingSectionVisited)
            {
                ExtractMapData(line, numOfTasks, validations);
            }

            return LocalCommunication;
        }

        private void ExtractMapData(string line, int numOfTasks, Validations validations)
        {

            if (LocalCommunication.MapMatrix == null && line.StartsWith(CffKeywords.LOCAL_COMMUNICATION_MAP))
            {
                // Check the MAP format is valid
                bool isValidMapFormat = RegexValidation.RegexMap(line, validations);
                string returnedMap = null;

                if (isValidMapFormat)
                {
                    returnedMap = validations.ValidateStringPair(
                        line,
                        TaffKeywords.ALLOCATION_MAP);
                }

                if (returnedMap != null)
                {
                    LocalCommunication.MapData = new Map(returnedMap);
                    LocalCommunication.MapMatrix = LocalCommunication.MapData.ConvertToMatrix(
                        numOfTasks,
                        numOfTasks,
                        validations);
                }
            }
        }

        public void ValidateLocalCommunication(Validations validations)
        {
            if (LocalCommunication.MapData == null || LocalCommunication.MapMatrix == null)
            {
                Error error = new Error();

                error.Message = $"The map data for {CffKeywords.OPENING_LOCAL_COMMUNICATION} is missing";
                error.ActualValue = "null";
                error.ExpectedValue = "The map value";
                error.LineNumber = "";
                error.Filename = validations.Filename;
                error.ErrorCode = ErrorCode.MISSING_VALUE;

                validations.ErrorValidationManager.Errors.Add(error);
            }
        }
    }
}
