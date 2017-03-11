using DotNetCache.Logic.Experiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCache.Logic.Services
{
    public class ExperimentService
    {
        private readonly ExperimentBase experiment;
        public ExperimentService(ExperimentBase experiment)
        {
            this.experiment = experiment;
        }

        public string Start()
        {
            experiment.Start();
            return experiment.Log;
        }
    }
}
