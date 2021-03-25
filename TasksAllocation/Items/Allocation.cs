using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils;

namespace TasksAllocation.Items
{
    class Allocation
    {
        public int ID { get; set; }
        public Map MapData { get; set; }

        public Allocation(int id, Map mapData)
        {
            ID = id;
            MapData = mapData;
        }
    }
}
