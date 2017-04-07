using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment02 : ExperimentBase
    {
        public Experiment02(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                for (int i = 0; i < 3; i++)
                {
                    StartTime();
                    var res = db.Orders.Cacheable().Where(o => o.O_TOTALPRICE < 1000).ToList();
                    Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(),DemoDataDbContext.Cache.Count));
                }
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return ExperimentSettings.Default;
        }
    }
}
