using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation.Utils.FilesManipulation
{
    public class FilesUtils
    {
        public static string ReplaceExtension(string filePath, string newExenstion)
        {
            string filename = Path.GetFileName(filePath);
            string extractedExtension = Path.GetExtension(filePath);
            string filenameWithNewExtension = filename.Replace(extractedExtension, $".{newExenstion}");

            return filenameWithNewExtension;
        }

        public static int ExtractNumber(int number, string line, string keyword, Validations validations)
        {
            if(number < 0 && line.StartsWith(keyword))
            {
                int extractedNumber = validations.ValidateIntegerPair(line, keyword);

                return extractedNumber;
            }

            return number;
        }
    }
}
