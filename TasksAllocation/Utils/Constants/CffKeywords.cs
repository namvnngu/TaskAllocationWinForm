using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TasksAllocation.Utils.Constants
{
    class CffKeywords
    {
        public static string FILE_EXTENSION = "cff";
        public static string OPENING_LOGFILE = "LOGFILE";
        public static string CLOSING_LOGFILE = "END-LOGFILE";
        public static string DEFAULT_LOGFILE = "DEFAULT";

        public static string OPENING_LIMITS = "LIMITS";
        public static string CLOSING_LIMITS = "END-LIMITS";
        public static string MINIMUM_TASKS = "MINIMUM-TASKS";
        public static string MAXIMUM_TASKS = "MAXIMUM-TASKS";
        public static string MINIMUM_PROCESSORS = "MINIMUM-PROCESSORS";
        public static string MAXIMUM_PROCESSORS = "MAXIMUM-PROCESSORS";
        public static string MINIMUM_PROCESSOR_FREQUENCIES = "MINIMUM-PROCESSOR-FREQUENCIES";
        public static string MAXIMUM_PROCESSOR_FREQUENCIES = "MAXIMUM-PROCESSOR-FREQUENCIES";
        public static string MINIMUM_RAM = "MINIMUM-RAM";
        public static string MAXIMUM_RAM = "MAXIMUM-RAM";
        public static string MINIMUM_DOWNLOAD = "MINIMUM-DOWNLOAD";
        public static string MAXINUM_DOWNLOAD = "MAXIMUM-DOWNLOAD";
        public static string MINIMUM_UPLOAD = "MINIMUM-UPLOAD";
        public static string MAXIMUM_UPLOAD = "MAXIMUM-UPLOAD";

        public static string OPENING_PROGRAM = "PROGRAM";
        public static string CLOSING_PROGRAM = "END-PROGRAM";
        public static string PROGRAM_DURATION = "DURATION";
        public static string PROGRAM_TASKS = "TASKS";
        public static string PROGRAM_PROCESSORS = "PROCESSORS";

        public static string OPENING_TASKS = "TASKS";
        public static string CLOSING_TASKS = "END-TASKS";

        public static string OPENING_TASK = "TASK";
        public static string CLOSING_TASK = "END-TASK";
        public static string TASK_ID = "ID";
        public static string TASK_RUNTIME = "RUNTIME";
        public static string TASK_REFERENCE_FREQUENCY = "REFERENCE-FREQUENCY";
        public static string TASK_RAM = "RAM";
        public static string TASK_DOWNLOAD = "DOWNLOAD";
        public static string TASK_UPLOAD = "UPLOAD";

        public static string OPENING_PROCESSORS = "PROCESSORS";
        public static string CLOSING_PROCESSORS = "END-PROCESSORS";

        public static string OPENING_PROCESSOR = "PROCESSOR";
        public static string CLOSING_PROCESSOR = "END-PROCESSOR";
        public static string PROCESSOR_ID = "ID";
        public static string PROCESSOR_TYPE = "TYPE";
        public static string PROCESSOR_FREQUENCY = "FREQUENCY";
        public static string PROCESSOR_RAM = "RAM";
        public static string PROCESSOR_DOWNLOAD = "DOWNLOAD";
        public static string PROCESSOR_UPLOAD = "UPLOAD";

        public static string OPENING_PROCESSOR_TYPES = "PROCESSOR-TYPES";
        public static string CLOSING_PROCESSOR_TYPES = "END-PROCESSOR-TYPES";

        public static string OPENING_PROCESSOR_TYPE = "PROCESSOR-TYPE";
        public static string CLOSING_PROCESSOR_TYPE = "END-PROCESSOR-TYPE";
        public static string PROCESSOR_TYPE_NAME = "NAME";
        public static string PROCESSOR_TYPE_C2 = "C2";
        public static string PROCESSOR_TYPE_C1 = "C1";
        public static string PROCESSOR_TYPE_C0 = "C0";

        public static string OPENING_LOCAL_COMMUNICATION = "LOCAL-COMMUNICATION";
        public static string CLOSING_LOCAL_COMMUNICATION = "END-LOCAL-COMMUNICATION";
        public static string LOCAL_COMMUNICATION_MAP = "MAP";

        public static string OPENING_REMOTE_COMMUNICATION = "REMOTE-COMMUNICATION";
        public static string CLOSING_REMOTE_COMMUNICATION = "END-REMOTE-COMMUNICATION";
        public static string REMOTE_COMMUNICATION_MAP = "MAP";

        public static string COMMENT = "//";

        public static Dictionary<string, string> KEYWORD_DICT = (new CffKeywords())
                                                .GetType()
                                                .GetFields(BindingFlags.Public | BindingFlags.Static)
                                                .Where(f => f.FieldType == typeof(string))
                                                .ToDictionary(f => f.Name,
                                                              f => (string)f.GetValue(null));
    }
}
