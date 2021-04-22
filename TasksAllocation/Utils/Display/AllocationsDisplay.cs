using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Files;
using TasksAllocation.Components;

namespace TasksAllocation.Utils.Display
{
    class AllocationsDisplay
    {
        public static string Display(List<AllocationDisplay> allocationDisplays, Configuration configuration)
        {
            StringBuilder renderedText = new StringBuilder();

            foreach (AllocationDisplay allocationDisplay in allocationDisplays)
            {
                int allocationID = allocationDisplay.ID;
                double allocationRuntime = allocationDisplay.Runtime;
                double allocationEnergy = allocationDisplay.Energy;
                List<ProcessorAllocation> prococessAllocations = allocationDisplay.ProcessorAllocations;

                // Allocation Info
                renderedText.Append("<br><table>");
                renderedText.Append("<tr>");
                renderedText.Append($"<th colspan=\"4\">Allocation ID = {allocationID}, Runtime = {allocationRuntime}, Energy = {allocationEnergy}</th>");
                renderedText.Append("</tr>");
                
                // Header
                renderedText.Append("<tr>");
                renderedText.Append($"<th style=\"text-align:left\">Allocation</th>");
                renderedText.Append($"<th style=\"text-align:left\">RAM</th>");
                renderedText.Append($"<th style=\"text-align:left\">Download</th>");
                renderedText.Append($"<th style=\"text-align:left\">Upload</th>");
                renderedText.Append("</tr>");

                for (int processorAllocationNum = 0; processorAllocationNum < prococessAllocations.Count; processorAllocationNum++)
                {
                    ProcessorAllocation processorAllocation = prococessAllocations[processorAllocationNum];
                    string processTaskAllocation = processorAllocation.Allocation;
                    int processTaskAllocationRAM = processorAllocation.RAM;
                    int processTaskAllocationDownload = processorAllocation.Download;
                    int processTaskAllocationUpload = processorAllocation.Upload;
                    Processor processor = configuration.Processors[processorAllocationNum];
                    int processRAM = processor.RAM;
                    int processDownload = processor.Download;
                    int processUpload = processor.Upload;

                    // Processor Allocation Info
                    renderedText.Append("<tr>");
                    renderedText.Append($"<td>{processTaskAllocation}</td>");
                    renderedText.Append($"<td>{processTaskAllocationRAM}/{processRAM} GB</td>");
                    renderedText.Append($"<td>{processTaskAllocationDownload}/{processDownload} Gbps</td>");
                    renderedText.Append($"<td>{processTaskAllocationUpload}/{processUpload} Gbps</td>");
                    renderedText.Append("</tr>");
                }

                renderedText.Append("</table>");
            }

            return renderedText.ToString();
        }
    }
}
