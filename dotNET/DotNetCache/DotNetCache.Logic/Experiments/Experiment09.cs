using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests first level cache after UPDATE.
    /// </summary>
    public class Experiment09 : ExperimentBase
    {
        public Experiment09(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            int customerId;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                customerId = db.Customers.Cacheable().First().C_CUSTKEY;

                StartTime();
                var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == customerId);
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));
            }
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                var customer = db.Customers.Find(customerId);
                customer.C_COMMENT = "LOL";
                db.SaveChanges();
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                customerId = db.Customers.Cacheable().First().C_CUSTKEY;

                StartTime();
                var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == customerId);
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
