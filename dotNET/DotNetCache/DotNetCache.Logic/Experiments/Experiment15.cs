using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    /**
     * 
     * Experiment checks the functionality of the blacklist.
     * a) query executed and cached for the 1st time
     * b) cache is freed and the query is set into the black list
     * c) the same query is executed again and it should not be cached. 
     * d) blacklist entry is removed
     * f) the same query is executed again and it be cached. 
     */
    public class Experiment15 : ExperimentBase
    {
        public override ExperimentSettings GetSettings()
        {
            return new ExperimentSettings();
        }

        public override List<ExperimentResult> Start()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
               
                Console.WriteLine("Experiment 15 - Blacklist");
                //a)
                var customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();

               /* var sql =  ((System.Data.Entity.Core.Objects.ObjectQuery) db.Customers.Cacheable()
                    .Where(c => c.C_CUSTKEY < 750)).ToTraceString();*/

                var sql = db.Customers.Cacheable()
                    .Where(c => c.C_CUSTKEY < 750)
                    .ToString();
                Console.WriteLine("Cached: " + InMemoryCache.LastCached); // Should be True

                //b)
                DemoDataDbContext.Cache.ClearCache();
                DemoDataDbContext.Cache.Purge();
                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                BlacklistedQueriesRegistrar.Instance.AddBlacklistedQuery(objectContext.MetadataWorkspace, sql);

                //c)
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();

                Console.WriteLine("Cached: " + InMemoryCache.LastCached); // Should be False

                //d)

                BlacklistedQueriesRegistrar.Instance.RemoveBlacklistedQuery(objectContext.MetadataWorkspace, sql);

                //e)

                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                customers = db.Customers.Cacheable().Where(c => c.C_CUSTKEY < 750).ToList();
                Console.WriteLine("Cached: " + InMemoryCache.LastCached); // Should be True
            }

            return Results;
        }

        public Experiment15(string connectionString) : base(connectionString)
        {
        }
    }
}
