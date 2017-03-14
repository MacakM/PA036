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

        public string Start()
        {
            experiment.Start();
            return experiment.Log;
        }
    }
}
