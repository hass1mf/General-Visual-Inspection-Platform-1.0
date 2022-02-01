using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidiCore.DataType
{
    public class AqTaskResult
    {
        public AqTaskResult()
        {
            this.TaskIndex = 0;
            this.ModuleResultDictionary = new Dictionary<string, AqModuleResult>();
        }

        public Dictionary<string, AqModuleResult> ModuleResultDictionary
        {
            get;
            set;
        }

        public int TaskIndex
        {
            get;
            set;
        }

    }
}