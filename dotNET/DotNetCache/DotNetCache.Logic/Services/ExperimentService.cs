using System.Collections.Generic;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.Logic.Experiments;
using EFCache;

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
            DemoDataDbContext.Cache.ClearCache();
            DemoDataDbContext.Cache.Purge();
            return experiment.Start();
        }
    }
}
