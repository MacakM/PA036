using System.Collections.Generic;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.Logic.Experiments;

namespace DotNetCache.Logic.Services
{
    public class ExperimentService
    {
        public List<ExperimentResult> Start(ExperimentBase experiment)
        {
            DemoDataDbContext.Cache.ClearCache();
            DemoDataDbContext.Cache.Purge();
            experiment.PrepareSettings();
            return experiment.Start();
        }

        public List<Logic.Experiments02.ExperimentResult> StartExtra(Experiments02.ExperimentBase experiment)
        {
            DemoDataDbContext.Cache.ClearCache();
            DemoDataDbContext.Cache.Purge();
            experiment.PrepareSettings();
            return experiment.StartExperiment();
        }
    }
}
