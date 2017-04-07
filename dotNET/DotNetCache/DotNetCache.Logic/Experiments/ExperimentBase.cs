using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public abstract class ExperimentBase
    {
        private Stopwatch stopwatch;
        private DateTime _lastQuery;
        protected string ConnectionString;
        protected List<ExperimentResult> Results = new List<ExperimentResult>();
        public ExperimentSettings ExperimentSettings { get; set; }
        
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
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, DemoDataDbContext.Cache);
                size = s.Length;
            }
            return (size / 1024f) / 1024f;
        }
    }
}
