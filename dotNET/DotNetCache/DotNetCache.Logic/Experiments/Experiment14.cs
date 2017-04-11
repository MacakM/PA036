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
    /// Tests second level cache after INSERT.
    /// TODO: NOT WORKING
    /// </summary>
    public class Experiment14 : ExperimentBase
    {
        public Experiment14(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var res = db.Orders.Cacheable().Where(o => o.O_TOTALPRICE < 1000).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));
            }
            Orders order;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                var maxOrderId = db.Orders.Max(o => o.O_ORDERKEY);
                order = new Orders
                {
                    O_CUSTKEY = 1,
                    O_ORDERKEY = maxOrderId + 1,
                    O_COMMENT = "furiously special f|",
                    O_ORDERPRIORITY = "3-MEDIUM",
                    O_TOTALPRICE = 851,
                    O_CLERK = "Peter",
                    O_ORDERDATE = DateTime.Now,
                    O_ORDERSTATUS = "O",
                    O_SHIPPRIORITY = 0
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var res = db.Orders.Cacheable().Where(o => o.O_TOTALPRICE < 1000).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));

                db.Orders.Remove(order);
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
