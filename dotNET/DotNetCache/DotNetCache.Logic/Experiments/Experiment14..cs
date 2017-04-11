using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCache.DataAccess.DemoDataContext;
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

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                var order = db.Orders.FirstOrDefault(o => o.O_TOTALPRICE < 1000);
                order.O_COMMENT = "LOL";
                db.SaveChanges();
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var res = db.Orders.Cacheable().Where(o => o.O_TOTALPRICE < 1000).ToList();
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
