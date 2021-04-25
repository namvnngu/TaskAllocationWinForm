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
    /// <summary>
    /// The superclass Commnucation allows subclasses
    /// to inherit and define their own behaviours.
    /// </summary>
    public class Communication
    {
        public Map MapData { get; set; }
        public PairSection OpeningClosingSection { get; set; }
        public string[,] MapMatrix { get; set; }

        public Communication()
        {
            MapData = null;
            MapMatrix = null;
        }

        public Communication(Map mapData)
        {
            MapData = mapData;
            MapMatrix = null;
        }

        public override string ToString()
        {
            if (MapMatrix == null)
            {
                return null;
            }

            StringBuilder displayedMap = new StringBuilder();
            int nRow = MapMatrix.GetLength(0);
            int nCol = MapMatrix.GetLength(1);

            for (int row = 0; row < nRow; row++)
            {
                for (int col = 0; col < nCol; col++)
                {
                    displayedMap.Append(MapMatrix[row, col] + " | ");
                }
                displayedMap.Append('\n');
            }

            return displayedMap.ToString();
        }
    }
}
