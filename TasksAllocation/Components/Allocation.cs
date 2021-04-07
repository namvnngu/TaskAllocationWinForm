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
            OpeningClosingSection = new PairSection(
                TaffKeywords.OPENING_ALLOCATION, 
                TaffKeywords.CLOSING_ALLOCATION);
        }

        public int CountTasks()
        {
            if (MapMatrix == null)
            {
                return -1;
            }

            int nRow = MapMatrix.GetLength(0);
            int nCol = MapMatrix.GetLength(1);
            int sumOfTask = 0;

            for (int row = 0; row < nRow; row++)
            {
                for(int col = 0; col < nCol; col++)
                {
                    int value;
                    int.TryParse(MapMatrix[row, col], out value);
                    sumOfTask += value;
                }
            }

            return sumOfTask;
        }
    }
}
