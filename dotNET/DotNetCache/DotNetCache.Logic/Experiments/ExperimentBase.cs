namespace DotNetCache.Logic.Experiments
{
    public abstract class ExperimentBase
    {
        public string Log { get; protected set; }
        public abstract void Start();
    }
}
