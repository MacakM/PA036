using System;
using System.Collections.Generic;
using System.IO;
using DotNetCache.Logic.Experiments;
using DotNetCache.Logic.Services;

namespace DotNetCache.Console
{
    public class Program
    {
        public static string ConnectionString200 =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "200M.mdf") + ";Integrated Security=True";
        public static string ConnectionString400 =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "400M.mdf") + ";Integrated Security=True";
        public static string ConnectionString800 =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "800M.mdf") + ";Integrated Security=True";
        public static string ConnectionString1000 =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "1000M.mdf") + ";Integrated Security=True";
        public static string ConnectionString2000 =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "2000M.mdf") + ";Integrated Security=True";

        private static readonly int[] Dbs = {2/*, 4, 8, 10, 20*/};

        private static List<Type> _experiments = new List<Type>
        {
            /* typeof(Experiment01),typeof(Experiment02),typeof(Experiment03),typeof(Experiment04),
             typeof(Experiment05),typeof(Experiment06),typeof(Experiment07),typeof(Experiment08),
             typeof(Experiment09),typeof(Experiment10),typeof(Experiment11),typeof(Experiment12),
             typeof(Experiment13), typeof(Experiment14), typeof(Experiment15), */typeof(Experiment16)/*, typeof(Experiment17), typeof(Experiment18)*/
        };
        public static void Main(string[] args)
        {
            var experiments = new List<List<ExperimentResult>>();
            foreach (var db in Dbs)
            {
                foreach (var experiment in _experiments)
                {
                    var copyConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, db + "00M.mdf") + ";Integrated Security=True";
                    experiments.Add(TodayTomorrowToyota(copyConnectionString, experiment));
                }

                for (var i = 0; i < experiments.Count; i++)
                {
                    System.Console.WriteLine("Db size: " + db + "Experiment " + (i + 1));
                    for (var j = 0; j < experiments[i].Count; j++)
                    {
                        System.Console.WriteLine("Query " + (j + 1) + ": " + experiments[i][j]);
                    }
                    System.Console.WriteLine();
                }
            }
            System.Console.ReadKey();
        }

        public static List<ExperimentResult> TodayTomorrowToyota(string connectionString, Type experimentType)
        {
            return new ExperimentService((ExperimentBase)Activator.CreateInstance(experimentType, connectionString)).Start();
        }
    }
}
