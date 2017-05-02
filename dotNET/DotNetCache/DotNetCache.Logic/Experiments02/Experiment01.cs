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
                this._timer.Start();
                
                //SELECT c.C_CUSTKEY FROM CUSTOMER c JOIN ORDERS o ON c.C_CUSTKEY = o.O_CUSTKEY 
                //JOIN LINEITEM l ON l.L_ORDERKEY = o.O_ORDERKEY JOIN PART p ON l.L_PARTKEY = p.P_PARTKEY GROUP BY c.C_CUSTKEY
                for (var i = 0; i < 2; i++)
                {
                    var stopwatch = Stopwatch.StartNew();
                    var res = db.Customers.Cacheable()
                        .Join(db.Orders.Cacheable(), cust => cust.C_CUSTKEY, ord => ord.O_CUSTKEY, (cust, ord) => ord)
                        .Join(db.LineItems.Cacheable(), ord => ord.O_ORDERKEY, l => l.L_ORDERKEY, (ord, l) => l)
                        .Join(db.Parts.Cacheable(), l => l.L_PARTKEY, p => p.P_PARTKEY, (l, p) => p)
                        .Select(p => p.P_PARTKEY)
                        .ToList();            

                    stopwatch.Stop();
                    Console.WriteLine("Run " + i + " - time: " + (int)stopwatch.ElapsedMilliseconds + " ms - Cached: " + InMemoryCache.LastCached);

                    /* var res = db.Database.SqlQuery<int>("SELECT c.C_CUSTKEY FROM dbo.Customer c JOIN dbo.Orders o ON c.C_CUSTKEY = o.O_CUSTKEY " +
                                                    "JOIN dbo.LineItem l ON l.L_ORDERKEY = o.O_ORDERKEY JOIN dbo.Part p ON l.L_PARTKEY = p.P_PARTKEY GROUP BY c.C_CUSTKEY")
                                                    .ToList(); */
                }
            }
        }
    }
}
