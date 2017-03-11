using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using DotNetCache.DataAccess.DemoDataContext;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment01 : ExperimentBase
    {
        public override void Start()
        {
            int customerId;
            using (var db = new DemoDataDbContext())
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.ToList();
                customerId = customers.First().C_CUSTKEY;
            }

            using (var db = new DemoDataDbContext())
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.Find(customerId);
            }
        }
    }
}
