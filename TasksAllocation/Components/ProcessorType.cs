using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class ProcessorType
    {
        double C0 { get; set; }
        double C1 { get; set; }
        double C2 { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public ProcessorType(double coefficient2, double coefficient1, double coefficient0)
        {
            C2 = coefficient2;
            C1 = coefficient1;
            C0 = coefficient0;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_PROCESSOR_TYPE, 
                CffKeywords.CLOSING_PROCESSOR_TYPE);
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
