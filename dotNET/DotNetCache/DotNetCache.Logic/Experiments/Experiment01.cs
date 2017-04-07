﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment01 : ExperimentBase
    {
        public Experiment01(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            int customerId;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.Cacheable().ToList();
                customerId = customers.First().C_CUSTKEY;
                
                for (int i = 0; i < 3; i++)
                {
                    StartTime();
                    var res = db.Customers.Cacheable().First(c => c.C_CUSTKEY == customerId);
                    Results.Add(new ExperimentResult(DbQueryCached(), StopTime(), GetCacheSize()));
                }
            }

            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings();
        }
    }
}
