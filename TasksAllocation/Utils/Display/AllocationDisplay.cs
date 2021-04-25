using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Display
{
    /// <summary>
    /// The class describes an individual allocation 
    /// data used for displaying in the main window.
    /// </summary>
    public class AllocationDisplay
    {
        public int ID;
        public double Runtime;
        public double Energy;
        public List<ProcessorAllocation> ProcessorAllocations;

        public AllocationDisplay()
        {

        }

        public AllocationDisplay(int id, double runtime, double energy, List<ProcessorAllocation> processorAllocations)
        {
            ID = id;
            Runtime = runtime;
            Energy = energy;
            ProcessorAllocations = processorAllocations;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine($"ID={ID}");
            text.AppendLine($"Runtime={Runtime}");
            text.AppendLine($"Energy={Energy}");

            foreach (ProcessorAllocation processorAllocation in ProcessorAllocations)
            {
                text.AppendLine($"{processorAllocation.Allocation} | RAM={processorAllocation.RAM} | " +
                    $"Upload={processorAllocation.Upload} | Donwload={processorAllocation.Download}");
            }

            return text.ToString();
        }
    }
}
