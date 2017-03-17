using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment01 : ExperimentBase
    {
        public Experiment01(string connectionString) : base(connectionString)
        {
        }

        public override void Start()
        {
            int customerId;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.ToList();
                customerId = customers.First().C_CUSTKEY;
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.Find(customerId);
            }
        }
    }
}
