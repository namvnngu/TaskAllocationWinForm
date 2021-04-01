using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TasksAllocation.Utils.FilesManipulation
{
    class Files
    {
        public static string ReplaceExtension(string filePath, string newExenstion)
        {
            string filename = Path.GetFileName(filePath);
            string extractedExtension = Path.GetExtension(filePath);
            string filenameWithNewExtension = filename.Replace(extractedExtension, $".{newExenstion}");

            return filenameWithNewExtension;
        }
    }
}
