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
    class LocalCommunication
    {
        public Map MapData { get; set; }
        public PairSection OpeningClosingSection { get; set; }
        public string[,] MapMatrix { get; set; }

        public LocalCommunication(Map mapData)
        {
            MapData = mapData;
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_LOCAL_COMMUNICATION, 
                CffKeywords.CLOSING_LOCAL_COMMUNICATION);
            MapMatrix = null;
        }
    }
}
