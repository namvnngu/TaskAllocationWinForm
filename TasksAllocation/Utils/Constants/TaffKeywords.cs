﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Constants
{
    class TaffKeywords
    {
        public static string FILE_EXTENSION = "taff";
        public static string OPENING_CONFIG_DATA = "CONFIGURATION-DATA";
        public static string CLOSING_CONFIG_DATA = "END-CONFIGURATION-DATA";
        public static string CONFIG_FILENAME = "FILENAME";

        public static string OPENING_ALLOCATIONS = "ALLOCATIONS";
        public static string CLOSING_ALLOCATIONS = "END-ALLOCATIONS";
        public static string ALLOCATIONS_COUNT = "COUNT";
        public static string ALLOCATIONS_TASKS = "TASKS";
        public static string ALLOCATIONS_PROCESSORS = "PROCESSORS";

        public static string OPENING_ALLOCATION = "ALLOCATION";
        public static string CLOSING_ALLOCATION = "END-ALLOCATION";
        public static string ALLOCATION_ID = "ID";
        public static string ALLOCATION_MAP = "MAP";
    }
}