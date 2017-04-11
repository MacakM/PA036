using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.DataAccess.DemoDataEntities;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests first level cache after DELETE.
    /// TODO: NOT WORKING
    /// </summary>
    public class Experiment11 : ExperimentBase
    {
        public Experiment11(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            int maxCustomerId;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                maxCustomerId = db.Customers.Max(o => o.C_CUSTKEY) + 1;
                var customer = new Customer
                {
                    C_CUSTKEY = maxCustomerId,
                    C_NATIONKEY = 1,
                    C_NAME = "Martin",
                    C_ADDRESS = "Brno",
                    C_COMMENT = "Best customer",
                    C_ACCTBAL = 0,
                    C_PHONE = "12345",
                    C_MKTSEGMENT = "lol"
                };
                db.Customers.Add(customer);
                db.SaveChanges();

                StartTime();
                var res = db.Customers.Cacheable().FirstOrDefault(c => c.C_CUSTKEY == maxCustomerId);
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var res = db.Customers.Cacheable().FirstOrDefault(c => c.C_CUSTKEY == maxCustomerId);
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
