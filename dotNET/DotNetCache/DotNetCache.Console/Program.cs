using System.Data.SqlClient;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.Logic.Experiments;
using DotNetCache.Logic.Services;

namespace DotNetCache.Console
{
    public class Program
    {
        public const string ConnectionString =
            "paste cs here";
        public static void Main(string[] args)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open(); // throws if invalid
            }
            DemoDataDbContext context = new DemoDataDbContext(ConnectionString);
            context.Database.Connection.Open();

         /*   var result = context.Database.SqlQuery<Customer>("SELECT TOP (1000) [C_CUSTKEY],[C_NAME],[C_ADDRESS],[C_NATIONKEY] ,[C_PHONE],[C_ACCTBAL] ,[C_MKTSEGMENT] ,[C_COMMENT] FROM[Test200M].[dbo].[CUSTOMER]").ToList();*/

                /*     var result = context.Database.SqlQuery<Customer>("SELECT TOP (1000) * FROM[Test200M].[dbo].[CUSTOMER]").ToList(); */
            var service = new ExperimentService(new Experiment01());
            System.Console.WriteLine(service.Start());
        }
    }
}
