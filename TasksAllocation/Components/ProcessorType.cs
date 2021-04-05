using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Components
{
    class ProcessorType
    {
        double C0 { get; set; }
        double C1 { get; set; }
        double C2 { get; set; }

        public ProcessorType(double coefficient2, double coefficient1, double coefficient0)
        {
            C2 = coefficient2;
            C1 = coefficient1;
            C0 = coefficient0;
        }

        public double EnergyPerSecond(double frequency)
        {
            return (C2 * frequency * frequency + C1 * frequency + C0);
        }

        public double Energy(double frequency, double runtime)
        {
            return (EnergyPerSecond(frequency) * runtime);
        }
    }
}
