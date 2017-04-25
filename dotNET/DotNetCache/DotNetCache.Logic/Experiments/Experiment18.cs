using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment18 : ExperimentBase
    {
        public Experiment18(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                var customers = db.Customers.Cacheable().Where(c => c.C_ACCTBAL > 10 && c.C_ACCTBAL < 100).ToList();
                foreach (var customer in customers)
                {
                   var orders = customer.Orderses.ToList();
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
