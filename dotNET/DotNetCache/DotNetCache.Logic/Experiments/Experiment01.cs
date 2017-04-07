using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests if I can get SELECT of the same row from cache.
    /// </summary>
    public class Experiment01 : ExperimentBase
    {
        public Experiment01(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            int customerId;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                customerId = db.Customers.Cacheable().First().C_CUSTKEY;
                
                for (int i = 0; i < 3; i++)
                {
                    StartTime();
                    var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == customerId);
                    Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));
                }
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings
            {
                CachePurgeInterval = TimeSpan.Zero,
                MaxCacheEntries = 0,
                MaxCacheSizeInMegaBytes = 0,
                RelativeCacheEntryValidity = TimeSpan.Zero
            };
        }
    }
}
