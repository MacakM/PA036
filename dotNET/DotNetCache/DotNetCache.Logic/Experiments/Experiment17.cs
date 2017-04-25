using System;
using System.Collections.Generic;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment17 : ExperimentBase
    {
        public Experiment17(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                var customers = db.Customers.Cacheable().GroupJoin(db.Orders, customer => customer.C_CUSTKEY, order => order.O_CUSTKEY, (customer, order) => new {customer, order}).Where(c => c.customer.C_ACCTBAL > 10 && c.customer.C_ACCTBAL < 100).ToList()
                
                foreach (var customer in customers)
                { 
                    var orders = customer.order.ToList();
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
