using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace DotNetCache.Logic.Experiments02
{
    [DataContract]
    public class ExperimentResult
    {
        [DataMember]
        public double CpuUsage { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime Time { get; set; }
        [DataMember]
        public double MemorySize { get; set; }
        [DataMember]
        public int CacheEntriesCount { get; set; }
        [DataMember]
        public string FormattedTime { get; set; }

        public ExperimentResult(DateTime Time, double MemorySize, int CacheEntriesCount,double CpuUsage)
        {
            this.CpuUsage = CpuUsage;
            this.Time = Time;
            this.MemorySize = MemorySize;
            this.CacheEntriesCount = CacheEntriesCount;
        }

        public override string ToString()
        {
            return "Cpu Usage: " + CpuUsage +
                ", Time: " + Time.ToLongTimeString() +
                ", Memory: " + (MemorySize - 0.00179195).ToString("0.00000000") + " MB" +
                ", Entry count: " + CacheEntriesCount;
        }


    }
}
