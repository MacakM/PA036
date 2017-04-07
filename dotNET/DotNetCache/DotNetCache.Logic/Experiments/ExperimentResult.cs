namespace DotNetCache.Logic.Experiments
{
    public class ExperimentResult
    {
        public ExperimentResult(bool cached = false, int time = 0, double memory = 0)
        {
            Cached = cached;
            Time = time;
            Memory = memory;
        }

        public bool Cached { get; set; }

        public int Time { get; set; }

        public double Memory { get; set; }

        public override string ToString()
        {
            return "Cached: " + Cached + ", Time: " + Time + " ms, Memory: " + Memory.ToString("0.00000000") + " MB";
        }
    }
}
