using System.Collections.Generic;
using DotNetCache.Logic.Experiments;
using System.Runtime.Serialization;

namespace DotNetCache.Api.Models
{
    [DataContract]
    public class ExperimentInfo
    {
        [DataMember]
        public int ExperimentId { get; set; }
        [DataMember]
        public List<ExperimentResult> Results { get; set; }
    }
}