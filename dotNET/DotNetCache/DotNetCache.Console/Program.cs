using System.Collections.Generic;
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
            var experiments = new List<List<ExperimentResult>>();

            experiments.Add(new ExperimentService(new Experiment01(ConnectionString)).Start());
            experiments.Add(new ExperimentService(new Experiment02(ConnectionString)).Start());
            experiments.Add(new ExperimentService(new Experiment03(ConnectionString)).Start());
            experiments.Add(new ExperimentService(new Experiment04(ConnectionString)).Start());
            experiments.Add(new ExperimentService(new Experiment05(ConnectionString)).Start());

            for (int i = 0; i < experiments.Count; i++)
            {
                System.Console.WriteLine("Experiment " + (i+1));
                for (int j = 0; j < experiments[i].Count; j++)
                {
                    System.Console.WriteLine("Query " + (j+1) + ": " + experiments[i][j]);
                }
                System.Console.WriteLine();
            }

            System.Console.ReadKey();
        }
    }
}
