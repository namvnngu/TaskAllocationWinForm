using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Display
{
    class AllocationDisplay
    {
        public int ID;
        public double Runtime;
        public double Energy;
        public List<ProcessorAllocation> Allocations;

        public AllocationDisplay()
        {

        }

        public AllocationDisplay(int id, double runtime, double energy, List<ProcessorAllocation> allocations)
        {
            ID = id;
            Runtime = runtime;
            Energy = energy;
            Allocations = allocations;
        }
    }
}
