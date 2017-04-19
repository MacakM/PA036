using System;
using System.Collections.Generic;

namespace DotNetCache.Logic.Experiments
{
    /**
     * Experiment has to prove cache inconsistency when db is updated from multiple sources
     * 1) PHP instance calls experiment 16
     * 2) .NET instance calls experiment 16
     * 3) PHP instance calls experiment 16 2nd time - changes the value
     * 4) .NET instance calls experiment 16 2nd time - gets invalid value 
     */
    public class Experiment16 : ExperimentBase
    {
        public Experiment16(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            throw new NotImplementedException();
        }

        public override ExperimentSettings GetSettings()
        {
            throw new NotImplementedException();
        }
    }
}
