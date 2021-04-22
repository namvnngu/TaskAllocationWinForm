using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksAllocation.Utils.Display
{
    class ProcessorAllocation
    {
        public string Allocation;
        public int RAM;
        public int Upload;
        public int Download;

        public ProcessorAllocation()
        {
            Allocation = null;
            RAM = 0;
            Upload = 0;
            Download = 0;
        }

        public ProcessorAllocation(string allocation, int ram, int upload, int download)
        {
            Allocation = allocation;
            RAM = ram;
            Upload = upload;
            Download = download;
        }
    }
}
