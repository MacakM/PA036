using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests how works cache when lowered max entries.
    /// </summary>
    public class Experiment07 : ExperimentBase
    {
        public Experiment07(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 500).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                StartTime();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 250).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                StartTime();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 250).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                StartTime();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 500).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                StartTime();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 250).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings(double.MaxValue, 1, int.MaxValue, TimeSpan.MaxValue);
        }
    }
}
