using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Utils.DataStructure
{
    class Map
    {
        public string Data { get; set; }

        public Map(string data)
        {
            Data = data;
        }

        public string[,] ConvertToMatrix(int nRow, int nCol)
        {
            string[,] matrix = new string[nRow, nCol];
            string[] rows = Data.Split(Symbols.SEMI_COLON);

            if (nRow != rows.Length)
            {
                
                throw new Exception("The number of processor is invalid");
            }

            return matrix;
        }
    }
}
