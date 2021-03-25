using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TasksAllocation.Items;

namespace TasksAllocation.Files
{
    class TaskAllocation
    {
        public string CffFilename { get; set; }
        public int Count { get; set; }
        public int NumberOfTasks { get; set; }
        public int NumberOfProcessors { get; set; }
        public List<Allocation> Allocations { get; set; }
        List<string> Errors { get; set; }

        public bool GetCffFilename(string taffFilename)
        {
            CffFilename = null;
            Errors = new List<string>();
            string line;
            StreamReader streamReader = new StreamReader(taffFilename);

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();

                if (line.StartsWith("FILENAME"))
                {
                    string[] lineData = line.Split('=');

                    if (lineData.Length == 2)
                    {
                        CffFilename = lineData[1].Trim('"');

                        if (CffFilename.Length > 0)
                        {
                            if (CffFilename.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
                            {
                                string filePath = Path.GetDirectoryName(taffFilename);
                                CffFilename = filePath + Path.DirectorySeparatorChar + CffFilename;

                                if (!File.Exists(CffFilename))
                                {
                                    // TODO: Error
                                    Errors.Add("CFF Filename Not Exist");
                                }
                            }
                            else
                            {
                                // TODO: Error
                                Errors.Add("Invalid Characters");
                            }
                        }
                        else
                        {
                            // TODO: Error
                            Errors.Add("No File Found");
                        }
                    }
                    else
                    {
                        // TODO: Error
                        Errors.Add("No Value for Keyword");
                    }
                }
            }

            streamReader.Close();

            return (Errors.Count == 0);
        }

        public bool Validate(string taffFilename)
        {
            Errors = new List<string>();

            return (Errors.Count == 0);
        }
    }
}
