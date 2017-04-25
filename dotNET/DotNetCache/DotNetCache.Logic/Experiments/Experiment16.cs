using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.DataAccess.DemoDataEntities;
using EFCache;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /**
     * Experiment has to prove cache inconsistency when db is updated from multiple sources
     * 1) .NET instance calls experiment 16, makes query - waits 10 second
     * 2) PHP instance calls experiment 16 - changes something within this 10 seconds
     * 3) .NET makes same query  - query shall hit the cache even if it is not valid
     */
    public class Experiment16 : ExperimentBase
    {
        private List<Customer> _cached;
        public Experiment16(string connectionString) : base(connectionString)
        {
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                Console.WriteLine("Getting data to cache");
                var customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                _cached = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                Console.WriteLine("Sleeping");
                Thread.Sleep(10000);
                Console.WriteLine("Woke up");
                _cached = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                Console.WriteLine("If true, and PHP done its job, data are inconsistent. Cached: " + InMemoryCache.LastCached); // If true, and PHP done its job, data are inconsistent.
            }
            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings();
        }
    }
}
