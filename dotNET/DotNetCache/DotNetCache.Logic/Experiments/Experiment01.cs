using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public class Experiment01 : ExperimentBase
    {

        private string _serverLogQuery = @"SELECT TOP 1 t.[text], s.last_execution_time
FROM sys.dm_exec_cached_plans AS p
INNER JOIN sys.dm_exec_query_stats AS s
   ON p.plan_handle = s.plan_handle
CROSS APPLY sys.dm_exec_sql_text(p.plan_handle) AS t
ORDER BY s.last_execution_time DESC;";
        public Experiment01(string connectionString) : base(connectionString)
        {
        }

        public override void Start()
        {
            int customerId;
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.Cacheable().ToList();
                customerId = customers.First().C_CUSTKEY;

                using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString+ "password=DBTestPass123;"))
                {
                    using (SqlCommand command = new SqlCommand(_serverLogQuery, conn))
                    {
                        conn.Open();
                        string result = (string)command.ExecuteScalar();
                    }
                }
            }

            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;
                var customers = db.Customers.First(c => c.C_CUSTKEY == customerId);
            }
        }
    }
}
