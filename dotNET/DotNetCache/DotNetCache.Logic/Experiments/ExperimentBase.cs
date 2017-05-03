using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;

namespace DotNetCache.Logic.Experiments
{
    public abstract class ExperimentBase
    {
        private Stopwatch stopwatch;
        private DateTime _lastQuery;
        protected string ConnectionString;
        protected List<ExperimentResult> Results = new List<ExperimentResult>();
        
        protected ExperimentBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string Log { get; protected set; }
        public abstract List<ExperimentResult> Start();
        public abstract ExperimentSettings GetSettings();

        protected bool DbQueryCached()
        {
            return InMemoryCache.LastCached;
        }

        protected void StartTime()
        {
            stopwatch = Stopwatch.StartNew();
        }

        protected int StopTime()
        {
            stopwatch.Stop();
            return (int)stopwatch.ElapsedMilliseconds;
        }

        protected double GetCacheSize()
        {
            return InMemoryCache.CacheSizeInMb;
        }

        public virtual void PrepareSettings()
        {
            ExperimentSettings.SetUpCache(GetSettings());
        }
    }
}
