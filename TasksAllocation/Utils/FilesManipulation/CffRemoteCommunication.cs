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
    class CffRemoteCommunication
    {
        public RemoteCommunication RemoteCommunication { get; set; }
        public PairSection RemoteCommunicationSection { get; set; }

        public CffRemoteCommunication()
        {
            RemoteCommunication = new RemoteCommunication();
            RemoteCommunicationSection = new PairSection(
                CffKeywords.OPENING_REMOTE_COMMUNICATION,
                CffKeywords.CLOSING_REMOTE_COMMUNICATION);
        }

        public RemoteCommunication ExtractRemoteCommunication(string line, int numOfTasks, Validations validations)
        {
            // Check whether the line starts opening/closing REMOTE-COMMUNICATION section
            // If yes, mark it exist
            RemoteCommunicationSection.MarkSection(line, Int16.Parse(validations.LineNumber));

            bool openingSectionVisited = RemoteCommunicationSection.ValidSectionPair[0];

            if (openingSectionVisited)
            {
                ExtractMapData(line, numOfTasks, validations);
            }

            return RemoteCommunication;
        }

        private void ExtractMapData(string line, int numOfTasks, Validations validations)
        {

            if (RemoteCommunication.MapMatrix == null && line.StartsWith(CffKeywords.REMOTE_COMMUNICATION_MAP))
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
                    RemoteCommunication.MapData = new Map(returnedMap);
                    RemoteCommunication.MapMatrix = RemoteCommunication.MapData.ConvertToMatrix(
                        numOfTasks,
                        numOfTasks,
                        validations);
                }
            }
        }

        public void ValidateRemoteCommunication(Validations validations)
        {
            if (RemoteCommunication.MapData == null || RemoteCommunication.MapMatrix == null)
            {
                Error error = new Error();

                error.Message = $"The map data for {CffKeywords.OPENING_REMOTE_COMMUNICATION} is missing";
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
