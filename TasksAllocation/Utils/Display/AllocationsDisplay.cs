﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Files;
using TasksAllocation.Components;
using TasksAllocation.Utils.Constants;
using System.IO;

namespace TasksAllocation.Utils.Display
{
    public class AllocationsDisplay
    {
        public static string Display(List<AllocationDisplay> allocationDisplays, Configuration configuration)
        {
            StringBuilder renderedText = new StringBuilder();

            foreach (AllocationDisplay allocationDisplay in allocationDisplays)
            {
                int allocationID = allocationDisplay.ID;
                double allocationRuntime = allocationDisplay.Runtime;
                double allocationEnergy = allocationDisplay.Energy;
                List<ProcessorAllocation> processorAllocations = allocationDisplay.ProcessorAllocations;

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

                // Body
                renderedText.Append(DisplayProcessorAllocation(processorAllocations, configuration));

                renderedText.Append("</table>");
            }

            return renderedText.ToString();
        }

        private static string DisplayProcessorAllocation(List<ProcessorAllocation> processorAllocations, Configuration configuration)
        {
            StringBuilder renderedText = new StringBuilder();

            for (int processorAllocationNum = 0; processorAllocationNum < processorAllocations.Count; processorAllocationNum++)
            {
                ProcessorAllocation processorAllocation = processorAllocations[processorAllocationNum];
                string processTaskAllocation = processorAllocation.Allocation;
                int processTaskAllocationRAM = processorAllocation.RAM;
                int processTaskAllocationDownload = processorAllocation.Download;
                int processTaskAllocationUpload = processorAllocation.Upload;
                Processor processor = configuration.Processors[processorAllocationNum];
                int processRAM = processor.RAM;
                int processDownload = processor.Download;
                int processUpload = processor.Upload;

                renderedText.Append("<tr>");
                renderedText.Append($"<td>{processTaskAllocation}</td>");
                renderedText.Append($"<td>{processTaskAllocationRAM}/{processRAM} GB</td>");
                renderedText.Append($"<td>{processTaskAllocationDownload}/{processDownload} Gbps</td>");
                renderedText.Append($"<td>{processTaskAllocationUpload}/{processUpload} Gbps</td>");
                renderedText.Append("</tr>");
            }

            return renderedText.ToString();
        }

        public static List<AllocationDisplay> DisplayInvalidAllocations(List<AllocationDisplay> allocationDisplays, int count)
        {
            if (allocationDisplays.Count == count || allocationDisplays.Count == 0)
            {
                return allocationDisplays;
            }

            Random rnd = new Random();
            List<AllocationDisplay> allocationDisplaysList = new List<AllocationDisplay>();

            for (int allocationDisplayNum = 0; allocationDisplayNum < count; allocationDisplayNum++)
            {
                int index = rnd.Next(0, allocationDisplays.Count);
                AllocationDisplay allocationDisplay = new AllocationDisplay();
                AllocationDisplay anotherAllocationDisplay = allocationDisplays[index];

                allocationDisplay.ID = allocationDisplayNum;
                allocationDisplay.Runtime = anotherAllocationDisplay.Runtime;
                allocationDisplay.Energy = anotherAllocationDisplay.Energy;
                allocationDisplay.ProcessorAllocations = anotherAllocationDisplay.ProcessorAllocations;

                allocationDisplaysList.Add(allocationDisplay);
            }

            return allocationDisplaysList;
        }
    }
}
