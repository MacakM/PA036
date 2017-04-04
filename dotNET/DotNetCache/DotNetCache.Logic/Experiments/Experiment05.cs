using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
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

                for (int i = 0; i < 3; i++)
                {
                    StartTime();
                    var res = customers.Take(10).Skip(i*10).ToList();
                    results.Add(new ExperimentResult(DbQueryCached(), StopTime(), DemoDataDbContext.Cache.Count));
                }

                for (int i = 0; i < 3; i++)
                {
                    StartTime();
                    var res = customers.Take(10).Skip(i * 10).ToList();
                    results.Add(new ExperimentResult(DbQueryCached(), StopTime(), DemoDataDbContext.Cache.Count));
                }
            }

            return results;
        }

    }
}
