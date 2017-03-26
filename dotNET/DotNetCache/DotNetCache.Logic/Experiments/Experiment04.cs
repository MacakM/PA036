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
                var ordersQuery = from ord in db.Orders.Cacheable()
                    join cust in db.Customers.Cacheable() on ord.O_CUSTKEY equals cust.C_CUSTKEY
                    select ord;
                var res = ordersQuery.ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime()));

                StartTime();
                var custQuery = from cust in db.Customers.Cacheable()
                    join nation in db.Nations.Cacheable() on cust.C_NATIONKEY equals nation.N_NATIONKEY
                    select cust;
                var res2 = custQuery.ToList();
                results.Add(new ExperimentResult(DbQueryCached(), StopTime()));
            }

            return results;
        }

    }
}
