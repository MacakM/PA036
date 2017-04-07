using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests SELECT TOP 100 then TOP 150.
    /// </summary>
    public class Experiment05 : ExperimentBase
    {
        public Experiment05(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                var customers = db.Customers.Cacheable().OrderBy(c => c.C_CUSTKEY);

                StartTime();
                var res = customers.Take(100).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                StartTime();
                res = customers.Take(150).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return ExperimentSettings.Default;
        }
    }
}
