using System.Collections.Generic;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.Logic.Experiments;

namespace DotNetCache.Logic.Services
{
    public class ExperimentService
    {
        private readonly ExperimentBase _experiment;
        public ExperimentService(ExperimentBase experiment)
        {
            _experiment = experiment;
        }

        public List<ExperimentResult> Start()
        {
            DemoDataDbContext.Cache.ClearCache();
            DemoDataDbContext.Cache.Purge();
            _experiment.PrepareSettings();
            return _experiment.Start();
        }
    }
}
