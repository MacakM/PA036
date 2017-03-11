using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.Logic.Experiments
{
    public abstract class ExperimentBase
    {
        public string Log { get; protected set; }
        public abstract void Start();
    }
}
