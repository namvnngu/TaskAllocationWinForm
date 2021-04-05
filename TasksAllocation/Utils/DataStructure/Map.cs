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
        public string ErrorMessage { get; set; }

        public Map(string data)
        {
            Data = data;
            ErrorMessage = null;
        }

        public string[,] ConvertToMatrix(int nRow, int nCol)
        {
            string[,] matrixData = new string[nRow, nCol];
            string[] rows = Data.Split(Symbols.SEMI_COLON);

            if (rows.Length != nRow)
            {
                ErrorMessage = "The number of processor is invalid";

                return null;
            }

            for (int rowNumber = 0; rowNumber < rows.Length; rowNumber++)
            {
                string row = rows[rowNumber];
                string[] cols = row.Split(Symbols.COMMA);

                if (cols.Length != nCol)
                {
                    ErrorMessage = "The number of available task allocations per processor is insufficient";

                    return null;
                }

                for (int colNumber = 0; colNumber < cols.Length; colNumber++)
                {
                    matrixData[rowNumber, colNumber] = cols[colNumber];
                }
            }

            return matrixData;
        }
    }
}
