using System.Runtime.Serialization;

namespace DotNetCache.Logic.Experiments
{
    [DataContract]
    public class ExperimentResult
    {
        [DataMember]
        public bool Cached { get; set; }
        [DataMember]
        public int Time { get; set; }
        [DataMember]
        public double Memory { get; set; }
        [DataMember]
        public int EntryCount { get; set; }

        public ExperimentResult(bool cached = false, int time = 0, double memory = 0, int entryCount = 0)
        {
            Cached = cached;
            Time = time;
            Memory = memory;
            EntryCount = entryCount;
        }

        public override string ToString()
        {
            return "Cached: " + Cached + ", Time: " + Time + " ms, Memory: " + (Memory-0.00179195).ToString("0.00000000") + " MB" + ", Entry count: " + EntryCount;
        }
    }
}
