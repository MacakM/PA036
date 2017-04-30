using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;
using System.Diagnostics;
using DotNetCache.Logic.Experiments;

namespace DotNetCache.Logic.Experiments02
{
    public abstract class ExperimentBase
    {
        private DateTime _lastQuery;
        protected string ConnectionString;
        private List<ExperimentResult> Results;
        PerformanceCounter cpuCounter;
        ExperimentSettings _settings;

        protected ExperimentBase(string connectionString,ExperimentSettings ExperimentSettings)
        {
            ConnectionString = connectionString;
            _settings = ExperimentSettings;
            Results = new List<ExperimentResult>();
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public string Log { get; protected set; }
        public List<ExperimentResult> StartExperiment()
        {
            this.Start();
            return this.Results;
        }
        protected abstract void Start();

        protected void AddResult(int CachedItemsCount)
        {
            this.Results.Add(new ExperimentResult(DateTime.Now, GetCacheSize(), CachedItemsCount, cpuCounter.NextValue()));
        }

        protected bool DbQueryCached()
        {
            return InMemoryCache.LastCached;
        }

        protected double GetCacheSize()
        {
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, DemoDataDbContext.Cache);
                size = s.Length;
            }
            return (size / 1024f) / 1024f;
        }

        public virtual void PrepareSettings()
        {
            Experiments.ExperimentSettings.SetUpCache(_settings);
        }
    }
}
