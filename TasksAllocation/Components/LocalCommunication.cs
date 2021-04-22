using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.DataStructure;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    class LocalCommunication : Communication
    {
        public LocalCommunication()
        {
        }

        public LocalCommunication(Map mapData) : base(mapData)
        {
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_LOCAL_COMMUNICATION, 
                CffKeywords.CLOSING_LOCAL_COMMUNICATION);
        }
    }
}
