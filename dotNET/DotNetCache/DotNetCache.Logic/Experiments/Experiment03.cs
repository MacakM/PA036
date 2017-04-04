using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment03 : ExperimentBase
    {
        public Experiment03(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 500).ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime(), DemoDataDbContext.Cache.Count));

                StartTime();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 10).ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime(), DemoDataDbContext.Cache.Count));

                StartTime();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 1000).ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime(), DemoDataDbContext.Cache.Count));
            }

            return results;
        }


    }
}
