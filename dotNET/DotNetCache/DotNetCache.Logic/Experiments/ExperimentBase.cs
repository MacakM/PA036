using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using EFSecondLevelCache;

namespace DotNetCache.Logic.Experiments
{
    public abstract class ExperimentBase
    {
        private Stopwatch stopwatch;
        private DateTime _lastQuery;
        private string _serverLogQuery =
            @"SELECT TOP 1 x.last_execution_time FROM 
(SELECT TOP 20 t.[text], s.last_execution_time
FROM sys.dm_exec_cached_plans AS p
INNER JOIN sys.dm_exec_query_stats AS s
   ON p.plan_handle = s.plan_handle
CROSS APPLY sys.dm_exec_sql_text(p.plan_handle) AS t
WHERE dbid=5
ORDER BY s.last_execution_time DESC) x
WHERE x.text NOT LIKE '%INFORMATION_SCHEMA%'
AND x.text NOT LIKE '%sys.dm_exec_cached_plans%'
ORDER BY x.last_execution_time DESC;";

        protected string ConnectionString;
        protected List<ExperimentResult> results = new List<ExperimentResult>();

        protected ExperimentBase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string Log { get; protected set; }
        public abstract List<ExperimentResult> Start();

        protected bool DbQueryCached()
        {
            using (var db = new DemoDataDbContext(ConnectionString))
            {
                db.Database.Log = s => Log += s;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(_serverLogQuery, conn))
                    {
                        conn.Open();
                        DateTime result = (DateTime)command.ExecuteScalar();
                        int compare = DateTime.Compare(_lastQuery, result);
                        //Console.WriteLine(result.ToString("hh:mm:ss.fff") + "\n" + _lastQuery.ToString("hh:mm:ss.fff"));
                        _lastQuery = result;
                        return compare == 0;
                    }
                }
            }
        }

        protected void StartTime()
        {
            stopwatch = Stopwatch.StartNew();
        }

        protected int StopTime()
        {
            stopwatch.Stop();
            return (int)stopwatch.ElapsedMilliseconds;
        }
    }
}
