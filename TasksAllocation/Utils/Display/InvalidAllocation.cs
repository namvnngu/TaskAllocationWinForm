using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.Display
{
    class InvalidAllocation
    {
        public static List<string[]> ExtractAllocation(string taffFile)
        {
            StreamReader streamReader = new StreamReader(taffFile);
            List<string[]> allocations = new List<string[]>();
            string allocationID = "";
            string allocationMap = "";
            string line;

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();

                ExtractData(TaffKeywords.ALLOCATION_ID, line, ref allocationID);
                ExtractData(TaffKeywords.ALLOCATION_MAP, line, ref allocationMap);

                if (line == TaffKeywords.CLOSING_ALLOCATION)
                {
                    string[] allocation = { allocationID, allocationMap };
                    allocations.Add(allocation);

                    allocationID = "";
                    allocationMap = "";
                }
            }

            streamReader.Close();

            return allocations;
        }

        public static List<string[]> ExtractTasks(string cffFile)
        {
            StreamReader streamReader = new StreamReader(cffFile);
            List<string[]> tasks = new List<string[]>();
            List<string[]> processors = new List<string[]>();
            
            // Tasks
            string taskID = "";
            string taskRuntime = "";
            string taskReferenceFrequency = "";
            string taskRAM = "";
            string taskDownload = "";
            string taskUpload = "";
            string line;

            // Processors
            string processorId = "";
            string processorType = "";
            string processorFreq = "";
            string processorRAM = "";
            string processorDownload = "";
            string processorUpload = "";

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                line = line.Trim();

                // Tasks
                ExtractData(CffKeywords.TASK_ID, line, ref taskID);
                ExtractData(CffKeywords.TASK_RUNTIME, line, ref taskRuntime);
                ExtractData(CffKeywords.TASK_REFERENCE_FREQUENCY, line, ref taskReferenceFrequency);
                ExtractData(CffKeywords.TASK_RAM, line, ref taskRAM);
                ExtractData(CffKeywords.TASK_DOWNLOAD, line, ref taskDownload);
                ExtractData(CffKeywords.TASK_UPLOAD, line, ref taskUpload);

                if (line == CffKeywords.CLOSING_TASK)
                {
                    string[] task = { 
                        taskID,
                        taskRuntime,
                        taskReferenceFrequency,
                        taskRAM,
                        taskDownload,
                        taskUpload
                    };

                    tasks.Add(task);

                    taskID = "";
                    taskRuntime = "";
                    taskReferenceFrequency = "";
                    taskRAM = "";
                    taskDownload = "";
                    taskUpload = "";
                }

                // Processors
                ExtractData(CffKeywords.PROCESSOR_ID, line, ref taskID);
                ExtractData(CffKeywords.PROCESSOR_TYPE, line, ref taskRuntime);
                ExtractData(CffKeywords.PROCESSOR_FREQUENCY, line, ref taskReferenceFrequency);
                ExtractData(CffKeywords.PROCESSOR_RAM, line, ref taskRAM);
                ExtractData(CffKeywords.TASK_DOWNLOAD, line, ref taskDownload);
                ExtractData(CffKeywords.TASK_UPLOAD, line, ref taskUpload);
            }

            streamReader.Close();

            return tasks;
        }

        private static void ExtractData(string keyword, string line, ref string extractedData)
        {
            if (line.StartsWith(keyword))
            {
                string[] data = line.Split(Symbols.EQUALITY);
                const int N_LINE_DATA = 2;

                if (data.Length == N_LINE_DATA)
                {
                    extractedData = data[2];
                }
            }
        }
    }
}
