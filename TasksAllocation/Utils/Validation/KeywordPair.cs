using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Validation
{
    public class KeywordPair
    {
        public int Count { get; set; }
        public string OpeningKeyword { get; set; }
        public string ClosingKeyword { get; set; }

        public KeywordPair(string openingKeyword, string closingKeyword)
        {
            Count = 0;
            OpeningKeyword = openingKeyword;
            ClosingKeyword = closingKeyword;
        }

        public void CheckValidKeyword(string keyword)
        {
            if (OpeningKeyword == keyword || ClosingKeyword == keyword)
            {
                Count++;
            }
        }

        public int CalculateNumOfPair()
        {
            return Count / 2;
        }
    }
}
