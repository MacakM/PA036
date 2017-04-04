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

                // netusim coho s atu snazime akoze dosiahnut? zistit ci dotazovalo v druhom query len jednu tabulku? gl s tym

                StartTime();
                var ordersQuery = db.Orders.Cacheable()
                    .Join(db.Customers.Cacheable(), ord => ord.O_CUSTKEY, cust => cust.C_CUSTKEY, (ord, cust) => ord);
                var res = ordersQuery.ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize()));

                StartTime();
                var custQuery = db.Customers.Cacheable()
                    .Join(db.Nations.Cacheable(), cust => cust.C_NATIONKEY, nation => nation.N_NATIONKEY,
                        (cust, nation) => cust);
                var res2 = custQuery.ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize()));
            }

            return results;
        }

    }
}
