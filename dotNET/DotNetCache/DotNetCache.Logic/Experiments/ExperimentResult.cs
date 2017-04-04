using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.Logic.Experiments
{
    public class ExperimentResult
    {
        public ExperimentResult(bool cached = false, int time = 0, int memory = 0)
        {
            Cached = cached;
            Time = time;
            Memory = memory;
        }

        public bool Cached { get; set; }

        public int Time {get; set; }

        public int Memory { get; set; }

        public override string ToString()
        {
            return "Cached: " + Cached + ", Time: " + Time + " ms, Memory: " + Memory + " entries in cache";
        }
    }
}
