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
    public class Experiment00 : ExperimentBase
    {
        System.Timers.Timer _timer;
        public Experiment00(string connectionString, ExperimentSettings settings) : base(connectionString, settings)
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

                for (var i = 0; i < 3; i++)
                {
                    this._timer.Start();

                    for (var j = 1; j <= 30; j++)
                    {
                        var res = db.Customers.Cacheable()
                            .Join(db.Orders.Cacheable(), cust => cust.C_CUSTKEY, ord => ord.O_CUSTKEY,
                                (cust, ord) => new {Customers = cust, Orders = ord})
                            .Join(db.LineItems.Cacheable(), ord => ord.Orders.O_ORDERKEY, l => l.L_ORDERKEY,
                                (ord, l) => new {LineItems = l, Customers = ord.Customers, Orders = ord.Orders})
                            .Where(p => p.Customers.C_CUSTKEY < j + (8000 * 1))
                            .Select(p => p.Customers.C_NAME)
                            .ToList();

                        if (!DbQueryCached() && InMemoryCache.WasCached)
                            InMemoryCache.RealEntryCount += res.Count;

                        Console.WriteLine("Run " + (i+1) + "-" + (j) + " " + DbQueryCached() + " " + InMemoryCache.CacheSizeInMb + " " + InMemoryCache.RealEntryCount);
                    }

                    this._timer.Stop();
                }
            }
        }
    }
}
