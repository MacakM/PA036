namespace DotNetCache.Logic.Experiments
{
    public abstract class ExperimentBase
    {
        protected string ConnectionString;

        protected ExperimentBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string Log { get; protected set; }
        public abstract void Start();
    }
}
