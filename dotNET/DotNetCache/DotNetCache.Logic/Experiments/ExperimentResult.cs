namespace DotNetCache.Logic.Experiments
{
    public class ExperimentResult
    {
        public bool Cached { get; set; }
        public int Time { get; set; }
        public double Memory { get; set; }
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
