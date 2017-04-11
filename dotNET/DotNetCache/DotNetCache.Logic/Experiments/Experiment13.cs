using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.DataAccess.DemoDataEntities;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests first level cache after INSERT.
    /// TODO: NOT WORKING
    /// </summary>
    public class Experiment13 : ExperimentBase
    {
        public Experiment13(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            int maxCustomerId;
            Customer customer;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                maxCustomerId = db.Customers.Max(o => o.C_CUSTKEY) + 1;
                customer = new Customer
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
                var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == maxCustomerId);
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),
                    DemoDataDbContext.Cache.Count));
            }
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

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
