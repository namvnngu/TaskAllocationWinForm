using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;
using System.IO;

namespace TasksAllocation.Utils.Validation
{
    class Validations
    {
        public static bool CheckExtension(string filePath, string expectedExtension, ref ErrorManager errorManager)
        {
            string extractedExtension = Path.GetExtension(filePath);
            string[] error = new string[3];

            if (extractedExtension == "")
            {
                error[0] = "File extension cannot be found";
                error[1] = "null";
                error[2] = $".{expectedExtension}";
                errorManager.Errors.Add(error);

                return false;
            }

            if (extractedExtension == $".{expectedExtension}")
            {
                return true;
            }

            error[0] = "The file extension is invalid";
            error[1] = extractedExtension;
            error[2] = $".{expectedExtension}";
            errorManager.Errors.Add(error);

            return false;
        }

        public static bool CheckProcessedFileExists(string filename, string expectedFilename, ref ErrorManager errorManager)
        {
            if (filename == null)
            {
                string[] error = new string[3];
                error[0] = $"There is no expected file";
                error[1] = "null";
                error[2] = expectedFilename;
                errorManager.Errors.Add(error);

                return false;
            }

            return true;
        }

        public static string[] CheckPairKeyValue(string line, string key, string value, ref ErrorManager errorManager)
        {
            string[] lineData = line.Split(Symbols.EQUALITY);
            const int N_LINE_DATA = 2;

            if (lineData.Length == N_LINE_DATA)
            {
                return lineData;
            }

            string[] error = new string[3];
            error[0] = $"No value for {key} can be found";
            error[1] = "null";
            error[2] = $"{key}=\"{value}\"";
            errorManager.Errors.Add(error);

            return null;
        }

        public static string CheckTextValueExist(string text, string actualValue, string expectedValue, ref ErrorManager errorManager)
        {
            string value = text.Trim(Symbols.DOUBLE_QUOTE);

            if (value.Length > 0)
            {
                return value;
            }

            string[] error = new string[3];
            error[0] = "No valid file/text/value can be found";
            error[1] = actualValue;
            error[2] = expectedValue;
            errorManager.Errors.Add(error);

            return null;
        }

        public static bool CheckInvalidFileNameChars(string filePath, ref ErrorManager errorManager)
        {
            if (filePath.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
            {
                return true;
            }

            string[] error = new string[3];
            error[0] = "File path contains invalid characters";
            error[1] = filePath;
            error[2] = "Path must not contain <, >, :, \", /, \\, |, ?, *";
            errorManager.Errors.Add(error);

            return false;
        }

        public static bool CheckFileExists(string filePath, ref ErrorManager errorManager)
        {
            if (File.Exists(filePath))
            {
                return true;
            }

            string[] error = new string[3];
            error[0] = "File does not exist";
            error[1] = filePath;
            error[2] = "Please provide valid file path";
            errorManager.Errors.Add(error);

            return false;
        }
    }
}
