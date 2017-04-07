using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
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
                var customers = db.Customers.Cacheable().ToList();
                customerId = customers.First().C_CUSTKEY;
                
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
                CachePurgeInterval = 0,
                MaxCacheEntries = 0,
                MaxCacheSizeInMegaBytes = 0,
                RelativeCacheEntryValidity = TimeSpan.Zero

            };
        }
    }
}
