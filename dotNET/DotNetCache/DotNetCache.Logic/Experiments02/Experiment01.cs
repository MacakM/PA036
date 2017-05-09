using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using DotNetCache.Logic.Experiments;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;
using System.Threading;
using EFCache;

namespace DotNetCache.Logic.Experiments02
{
    public class Experiment01 : ExperimentBase
    {
        System.Timers.Timer _timer;
        public Experiment01(string connectionString,ExperimentSettings settings) : base(connectionString, settings)
        {
            _timer = new System.Timers.Timer(100);
        }

        protected override void Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                this._timer.Elapsed += (s, e) =>
                {
                    this.AddResult(DemoDataDbContext.Cache.Count);
                };
                
                //SELECT c.C_CUSTKEY FROM CUSTOMER c JOIN ORDERS o ON c.C_CUSTKEY = o.O_CUSTKEY 
                //JOIN LINEITEM l ON l.L_ORDERKEY = o.O_ORDERKEY JOIN PART p ON l.L_PARTKEY = p.P_PARTKEY
                for (var i = 0; i < 2; i++)
                {
                    this._timer.Start();

                    var res = db.Customers.Cacheable()
                        .Join(db.Orders.Cacheable(), cust => cust.C_CUSTKEY, ord => ord.O_CUSTKEY, (cust, ord) => new { Customers = cust, Orders = ord })
                        .Join(db.LineItems.Cacheable(), ord => ord.Orders.O_ORDERKEY, l => l.L_ORDERKEY, (ord, l) => new { LineItems = l, Customers = ord.Customers, Orders = ord.Orders })
                        .Join(db.Parts.Cacheable(), l => l.LineItems.L_PARTKEY, p => p.P_PARTKEY, (l, p) => new { Customers = l.Customers, LineItems = l.LineItems, Orders = l.Orders, Parts = p })
                        .Select(p => p.Customers.C_NAME)
                        .ToList();

                    this._timer.Stop();
                }
            }
        }
    }
}
