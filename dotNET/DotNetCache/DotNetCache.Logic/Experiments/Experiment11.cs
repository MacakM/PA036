using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.DataAccess.DemoDataEntities;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests first level cache after DELETE.
    /// TODO: THIS DOES NOT MAKE SENSE
    /// </summary>
    public class Experiment11 : ExperimentBase
    {
        public Experiment11(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                var customer = new Customer
                {
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

                var customerId = customer.C_CUSTKEY;

                StartTime();
                var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == customerId);
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                var customer = new Customer
                {
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

                var customerId = customer.C_CUSTKEY;

                StartTime();
                var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == customerId);
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));

                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return ExperimentSettings.Default;
        }
    }
}
