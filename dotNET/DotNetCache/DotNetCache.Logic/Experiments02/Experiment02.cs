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
    public class Experiment02 : ExperimentBase
    {
        System.Timers.Timer _timer;
        public Experiment02(string connectionString, ExperimentSettings settings) : base(connectionString, settings)
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

                //SELECT c.C_CUSTKEY, (SELECT COUNT(C_CUSTKEY) FROM CUSTOMER cc 
                //WHERE cc.C_CUSTKEY+2 = c.C_CUSTKEY+1) FROM CUSTOMER c;

                for (var i = 0; i < 2; i++)
                { 
                    this._timer.Start();

                    var res = db.Customers.Cacheable().Select(c => new
                    {
                        CustKey = c.C_CUSTKEY,
                        CustCount = db.Customers.Count(cc => cc.C_CUSTKEY + 2 == c.C_CUSTKEY + 1)
                    }).ToList();

                    this._timer.Stop();
                }
            }
        }
    }
}
