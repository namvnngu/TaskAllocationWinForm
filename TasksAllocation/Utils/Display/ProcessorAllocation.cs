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
        public string RAM;
        public string Upload;
        public string Download;

        public ProcessorAllocation()
        {

        }

        public ProcessorAllocation(string allocation, string ram, string upload, string download)
        {
            Allocation = allocation;
            RAM = ram;
            Upload = upload;
            Download = download;
        }
    }
}
