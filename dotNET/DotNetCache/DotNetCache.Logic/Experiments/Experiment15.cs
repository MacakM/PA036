using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /**
     * 
     * Experiment checks the functionality of the blacklist.
     * a) query executed and cached for the 1st time
     * b) cache is freed and the query is set into the black list
     * c) blacklist entry presence is checked
     * d) the same query is executed again and it should not be cached. 
     * e) blacklist entry is removed
     * d) the same query is executed again and it should not be cached. 
     */
    public class Experiment15 : ExperimentBase
    {
        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings();
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));
                BlacklistedQueriesRegistrar.Instance.AddBlacklistedQuery(new MetadataWorkspace(), "");
            }

            return Results;
        }

        public Experiment15(string connectionString) : base(connectionString)
        {
        }
    }
}
