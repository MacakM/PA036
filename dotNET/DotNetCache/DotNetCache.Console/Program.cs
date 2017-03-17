using System.Data.SqlClient;
using System.Linq;
using DotNetCache.DataAccess.DemoDataContext;
using DotNetCache.DataAccess.DemoDataEntities;
using DotNetCache.Logic.Experiments;
using DotNetCache.Logic.Services;

namespace DotNetCache.Console
{
    public class Program
    {
        public const string ConnectionString =
            "INSERT HERE";
        public static void Main(string[] args)
        {
            var service = new ExperimentService(new Experiment01(ConnectionString));
            System.Console.WriteLine(service.Start());
        }
    }
}
