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
        public string Name { get; set; }
        public double C0 { get; set; }
        public double C1 { get; set; }
        public double C2 { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public ProcessorType()
        {
            Name = null;
            C0 = -1;
            C1 = -1;
            C2 = -1;
        }

        public ProcessorType(string name, double coefficient2, double coefficient1, double coefficient0)
        {
            Name = name;
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

        public double CalculateEnergy(double frequency, double runtime)
        {
            return (EnergyPerSecond(frequency) * runtime);
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine($"PRCESSOR-TYPE-{CffKeywords.PROCESSOR_TYPE_NAME}={Name}");
            text.AppendLine($"{CffKeywords.PROCESSOR_TYPE_C2}={C2}");
            text.AppendLine($"{CffKeywords.PROCESSOR_TYPE_C1}={C1}");
            text.AppendLine($"{CffKeywords.PROCESSOR_TYPE_C0}={C0}");

            return text.ToString();
        }
    }
}
