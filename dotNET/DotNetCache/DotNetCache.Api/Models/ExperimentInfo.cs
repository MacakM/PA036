using System.Collections.Generic;
using DotNetCache.Logic.Experiments;

namespace DotNetCache.Api.Models
{
    public class ExperimentInfo
    {
        public ExperimentBase Experiment { get; set; }
        public List<ExperimentResult> Results { get; set; }
    }
}