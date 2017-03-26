using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    results.Add(new ExperimentResult(DbQueryCached(), StopTime()));
                }
            }

            return results;
        }

        
    }
}
