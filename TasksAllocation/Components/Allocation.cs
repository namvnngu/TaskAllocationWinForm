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
    class Allocation
    {
        public int ID { get; set; }
        public Map MapData { get; set; }
        public string[,] MapMatrix { get; set; }
        public PairSection OpeningClosingSection { get; set; }

        public Allocation(int id, Map mapData)
        {
            ID = id;
            MapData = mapData;
            MapMatrix = null;
            OpeningClosingSection = new PairSection(TaffKeywords.OPENING_ALLOCATION, TaffKeywords.CLOSING_ALLOCATION);
        }
    }
}
