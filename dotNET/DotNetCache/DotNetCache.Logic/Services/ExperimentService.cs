using System.Collections.Generic;
using DotNetCache.Logic.Experiments;

namespace DotNetCache.Logic.Services
{
    public class ExperimentService
    {
        private readonly ExperimentBase experiment;
        public ExperimentService(ExperimentBase experiment)
        {
            this.experiment = experiment;
        }

        public List<ExperimentResult> Start()
        {
            return experiment.Start();
        }
    }
}
