using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Constants
{
    class ErrorCode
    {
        public static int INVALID_KEYWORD = 10;
        public static int INVALID_VALUE = 11;
        public static int INVALID_ALLOCATION = 12;
        public static int INVALID_FORMAT = 13;
        public static int INVALID_RAM = 14;
        public static int INVALID_DOWNLOAD_SPEED = 15;
        public static int INVALID_UPLOAD_SPEED = 16;
        public static int INVALID_TAFF_FORMAT = 17;
        public static int INVALID_CFF_FORMAT = 18;
        public static int INVALID_EXTENSION = 19;
        public static int INVALID_FILE = 20;
        public static int INVALID_MAP = 21;

        public static int MISSING_VALUE = 30;
        public static int MISSING_SECTION = 31;
        public static int MISSING_FILE = 32;

        public static int NOT_FOUND = 41;

        public static int OUT_OF_LIMIT = 51;

        public static Dictionary<int, string> ErrorCodeDescription = new Dictionary<int, string>
        {
            { INVALID_KEYWORD, "Invalid keyword" },
            { INVALID_VALUE, "Invalid value" },
            { INVALID_ALLOCATION, "Invalid allocation" },
            { INVALID_FORMAT, "Invalid format" },
            { INVALID_RAM, "Invalid RAM" },
            { INVALID_DOWNLOAD_SPEED, "Invalid download speed" },
            { INVALID_UPLOAD_SPEED, "Invalid upload speed" },
            { INVALID_TAFF_FORMAT, "Invalid taff file format" },
            { INVALID_CFF_FORMAT, "Invalid cff file format" },
            { INVALID_EXTENSION, "Invalid file extension" },
            { INVALID_FILE, "Invalid file" },
            { MISSING_VALUE, "Missing value" },
            { MISSING_SECTION, "Missing section" },
            { MISSING_FILE, "Missing file" },
            { NOT_FOUND, "File cannot be found" },
            { INVALID_MAP, "The map data is invalid" },
            { OUT_OF_LIMIT, "The values is out of the required limit" }
        };
    }
}
