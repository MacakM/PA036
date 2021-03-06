﻿using Newtonsoft.Json;
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
        public long CacheEntriesCount { get; set; }
        [DataMember]
        public double DiskUsage { get; set; }
        [DataMember]
        public string FormattedTime { get; set; }

        public ExperimentResult(DateTime Time, double MemorySize, long CacheEntriesCount,double CpuUsage,double DiskUsage)
        {
            this.CpuUsage = CpuUsage;
            this.Time = Time;
            this.MemorySize = MemorySize;
            this.CacheEntriesCount = CacheEntriesCount;
            this.DiskUsage = DiskUsage;
        }
    }
}
