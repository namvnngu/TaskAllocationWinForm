using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.Constants;
using TasksAllocation.Utils.Validation;

namespace TasksAllocation.Utils.DataStructure
{
    public class Map
    {
        public string Data { get; set; }

        public Map(string data)
        {
            Data = data;
        }

        /// <summary>
        /// The method converts a map string into a two-dimensional
        /// matrix of n rows and m columns, and also throws an error 
        /// when the number of rows or columns is different from
        /// the predefined values.
        /// </summary>
        /// <param name="nRow"></param>
        /// <param name="nCol"></param>
        /// <param name="validations"></param>
        /// <returns></returns>
        public string[,] ConvertToMatrix(int nRow, int nCol, Validations validations)
        {
            if (nRow < 0 || nCol < 0)
            {
                return null;
            }

            string[,] matrixData = new string[nRow, nCol];
            string[] rows = Data.Split(Symbols.SEMI_COLON);

            if (rows.Length != nRow)
            {
                validations.CheckInvalidMap(
                    rows.Length.ToString(),
                    nRow.ToString(),
                    "processors");
                return null;
            }

            for (int rowNumber = 0; rowNumber < rows.Length; rowNumber++)
            {
                string row = rows[rowNumber];
                string[] cols = row.Split(Symbols.COMMA);

                if (cols.Length != nCol)
                {
                    validations.CheckInvalidMap(
                        cols.Length.ToString(),
                        nCol.ToString(),
                        "task allocation spaces per processor");
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
