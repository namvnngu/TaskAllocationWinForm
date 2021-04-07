using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;
using System.IO;

namespace TasksAllocation.Utils.FilesManipulation
{
    class TaffManipulation
    {
        public static string ExtractCff(string taffFilename, Validations validations)
        {
            string cffFilename = null;
            string line, expectedCffFilename = null;
            StreamReader streamReader = new StreamReader(taffFilename);
            PairSection openClosingConfigData = new PairSection(
                TaffKeywords.OPENING_CONFIG_DATA, 
                TaffKeywords.CLOSING_CONFIG_DATA);
            int lineNumber = 1;
            validations.Filename = taffFilename;

            // Check whether taffFilename has .taff extension
            validations.LineNumber = "";
            if (validations.CheckExtension(taffFilename, TaffKeywords.FILE_EXTENSION))
            {
                expectedCffFilename = Files.ReplaceExtension(
                    taffFilename, 
                    CffKeywords.FILE_EXTENSION);
            }
            else
            {
                return null;
            }


            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();
                validations.LineNumber = lineNumber.ToString();

                // Check whether the line starts Opening Configuration Data section 
                // If yes, mark it exist
                openClosingConfigData.StartWithOpeningSection(line, lineNumber);

                // Check whether the line starts Opening Configuration Data section 
                // If yes, mark it exist, and stop the loop
                if (openClosingConfigData.StartWithClosingSection(line, lineNumber))
                {
                    break;
                }

                // Check whether Opening Configuration Data section exists and
                // whether line start with the expected keyword, "FILENAME"
                if (openClosingConfigData.ValidSectionPair[0] && 
                    line.StartsWith(TaffKeywords.CONFIG_FILENAME))
                {
                    // Check whether the pair of key-value exists 
                    
                    string[] lineData = validations.CheckPairKeyValue(
                        line, 
                        TaffKeywords.CONFIG_FILENAME, 
                        $"\"{expectedCffFilename}\"");

                    if (lineData != null)
                    {
                        // Check whether the value in the above pair is valid
                        cffFilename = validations.CheckTextValueExist(
                            lineData[1], 
                            cffFilename,
                            expectedCffFilename);
                    }

                    // Check whether the value in the above pair has the valid extension (.cff),
                    // and check the filename has no invalia file character 
                    bool cffExtensionValid = cffFilename != null &&
                        validations.CheckExtension(cffFilename, CffKeywords.FILE_EXTENSION);
                    bool noInvalidaFilenameChars = cffExtensionValid &&
                        validations.CheckInvalidFileNameChars(cffFilename);
                    
                    if (noInvalidaFilenameChars)
                    {
                        string filePath = Path.GetDirectoryName(taffFilename);
                        cffFilename = $"{filePath}{Path.DirectorySeparatorChar}{cffFilename}";

                        validations.CheckFileExists(cffFilename);
                    }
                }

                lineNumber++;
            }

            streamReader.Close();

            // Checking whether the configuration data section exists
            openClosingConfigData.CheckValidPair(validations, taffFilename);

            // Check whether the cffFilename has been assigned a value or not
            validations.CheckProcessedFileExists(cffFilename, expectedCffFilename);

            return cffFilename;
        }
    }
}
