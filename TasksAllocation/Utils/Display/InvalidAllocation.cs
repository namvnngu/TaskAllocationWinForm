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
        public List<string[]> Allocations { get; set; }
        public List<string[]> Processors { get; set; }

        public InvalidAllocation()
        {
            Allocations = new List<string[]>();
            Processors = new List<string[]>();
        }

        public void ExtractAllocation(string taffFile)
        {
            StreamReader streamReader = new StreamReader(taffFile);
            
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
                    Allocations.Add(allocation);

                    allocationID = "";
                    allocationMap = "";
                }
            }

            streamReader.Close();
        }

        public void ExtractConfiguration(string cffFile)
        {
            StreamReader streamReader = new StreamReader(cffFile);
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

                // Processors
                ExtractData(CffKeywords.PROCESSOR_ID, line, ref processorId);
                ExtractData(CffKeywords.PROCESSOR_TYPE, line, ref processorType);
                ExtractData(CffKeywords.PROCESSOR_FREQUENCY, line, ref processorFreq);
                ExtractData(CffKeywords.PROCESSOR_RAM, line, ref processorRAM);
                ExtractData(CffKeywords.PROCESSOR_DOWNLOAD, line, ref processorDownload);
                ExtractData(CffKeywords.PROCESSOR_UPLOAD, line, ref processorUpload);

                if (line == CffKeywords.CLOSING_PROCESSOR)
                {
                    string[] processor = {
                        processorId,
                        processorType,
                        processorFreq,
                        processorRAM,
                        processorDownload,
                        processorUpload
                    };

                    Processors.Add(processor);

                    processorId = "";
                    processorType = "";
                    processorFreq = "";
                    processorRAM = "";
                    processorDownload = "";
                    processorUpload = "";
                }
            }

            streamReader.Close();
        }

        public void CalculateAllocationValues()
        {
            StringBuilder renderedText = new StringBuilder();

            for (int allocationNum = 0; allocationNum < Allocations.Count; allocationNum++)
            {
                // Allocation Info
                Random random = new Random();
                string[] allocation = Allocations[allocationNum];
                string allocationID = allocation[0];
                string[] processTaskAllocations = allocation[1].Split(Symbols.SEMI_COLON);
                double allocationRuntime = random.NextDouble() * 3;
                double allocationEnergy = random.NextDouble() * (500 - 200) + 200;
                
                // Header
                renderedText.Append("<br><table>");
                renderedText.Append("<tr>");
                renderedText.Append($"<th colspan=\"4\">Allocation ID = {allocationID}, Runtime = {allocationRuntime}, Energy = {allocationEnergy}</th>");
                renderedText.Append("</tr>");

                // Body
                for (int processAllocationNum = 0; processAllocationNum < processTaskAllocations.Length; processAllocationNum++)
                {
                    int processorRAM = Convert.ToInt32(Processors[processAllocationNum][3]);
                    int processorDownload = Convert.ToInt32(Processors[processAllocationNum][4]);
                    int processorUpload = Convert.ToInt32(Processors[processAllocationNum][5]);
                    string processTaskAllocation = processTaskAllocations[0];
                    int processTaskAllocationRAM = random.Next(0, processorRAM);
                    int processTaskAllocationDownload = random.Next(0, processorDownload);
                    int processTaskAllocationUpload = random.Next(0, processorUpload);

                    renderedText.Append("<tr>");
                    renderedText.Append($"<td>{processTaskAllocation}</td>");
                    renderedText.Append($"<td>{processTaskAllocationRAM}/{processorRAM} GB</td>");
                    renderedText.Append($"<td>{processTaskAllocationDownload}/{processorDownload} Gbps</td>");
                    renderedText.Append($"<td>{processTaskAllocationUpload}/{processorUpload} Gbps</td>");
                    renderedText.Append("</tr>");
                }
               
            }
        }

        private void ExtractData(string keyword, string line, ref string extractedData)
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
