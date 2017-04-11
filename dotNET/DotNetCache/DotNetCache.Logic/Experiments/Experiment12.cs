﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.DataAccess.DemoDataEntities;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /// <summary>
    /// Tests second level cache after DELETE.
    /// TODO: NOT WORKING
    /// </summary>
    public class Experiment12 : ExperimentBase
    {
        public Experiment12(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            Orders order;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                var maxOrderId = db.Orders.Max(o => o.O_ORDERKEY);
                order = new Orders
                {
                    O_CUSTKEY = 1,
                    O_ORDERKEY = maxOrderId + 1,
                    O_COMMENT = "Super",
                    O_ORDERPRIORITY = "High",
                    O_TOTALPRICE = 500
                };

                db.Orders.Add(order);
                db.SaveChanges();

                StartTime();
                var res = db.Orders.Cacheable().Where(o => o.O_TOTALPRICE < 1000).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                db.Orders.Remove(order);
                db.SaveChanges();
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                StartTime();
                var res = db.Orders.Cacheable().Where(o => o.O_TOTALPRICE < 1000).ToList();
                Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize(), DemoDataDbContext.Cache.Count));
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return ExperimentSettings.Default;
        }
    }
}
