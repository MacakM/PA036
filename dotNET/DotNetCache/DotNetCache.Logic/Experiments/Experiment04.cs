using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests if after A join B when I do B join C I don't need to load B from DB.
    /// </summary>
    public class Experiment04 : ExperimentBase
    {
        public Experiment04(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                
                StartTime();
                var ordersQuery = db.Orders.Cacheable()
                    .Join(db.Customers.Cacheable(), ord => ord.O_CUSTKEY, cust => cust.C_CUSTKEY, (ord, cust) => ord);
                var res = ordersQuery.ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));

                StartTime();
                var custQuery = db.Customers.Cacheable()
                    .Join(db.Nations.Cacheable(), cust => cust.C_NATIONKEY, nation => nation.N_NATIONKEY,
                        (cust, nation) => cust);
                var res2 = custQuery.ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return ExperimentSettings.Default;
        }
    }
}
