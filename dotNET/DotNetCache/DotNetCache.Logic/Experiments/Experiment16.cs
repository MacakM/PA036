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
     * 1) .NET instance calls experiment 16, makes query
     * 2)  DB is changed without using cache.
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
                var changeTo = DateTime.Now.Millisecond;
                Console.WriteLine("Getting data to cache");
                var customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                _cached = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                var key = _cached.ElementAt(0).C_CUSTKEY;
                Console.WriteLine("Sleeping");
                db.Database.ExecuteSqlCommand("UPDATE CUSTOMER SET C_NAME = '"+ changeTo+"' WHERE C_CUSTKEY='" + key + "'");
                var check = db.Customers.Cacheable().Where(c => c.C_CUSTKEY == key).ToList();
                Console.WriteLine("Woke up");
                var x = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                Console.WriteLine("If true, data are inconsistent. Cached: " + InMemoryCache.LastCached + " should equal if consistent: " + changeTo + " : " + x.Where(y=>y.C_CUSTKEY == key).First().C_NAME); 
            }
            return Results;
        }

        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings();
        }
    }
}
