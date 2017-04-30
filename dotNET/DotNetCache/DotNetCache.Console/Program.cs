using System;
using System.Collections.Generic;
using System.IO;
using DotNetCache.Logic.Experiments;
using DotNetCache.Logic.Services;
using System.Linq;
using Newtonsoft.Json;

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
             typeof(Experiment13), typeof(Experiment14), typeof(Experiment15), */typeof(Experiment18)/*, typeof(Experiment17), typeof(Experiment18)*/
        };
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Choose experiment set (1 for basic, 2 for extra)");
            var set = System.Console.ReadLine();
            switch (set)
            {
                case "1": RunBasicExperiments(); break;
                case "2":
                    var allowedSizes = new string[] { "200", "400", "800", "1000", "2000" };
                    System.Console.WriteLine("Choose db size (" + String.Join(",", allowedSizes) + ")");
                    var size = System.Console.ReadLine();
                    if (allowedSizes.Contains(size))
                    {
                        RunExtraExperiments(size);
                    }
                    else
                    {
                        IdiotAnnouncer();
                    }
                    break;
                default: IdiotAnnouncer(); break;
            }
            System.Console.WriteLine("gg");
            System.Console.ReadKey();
        }

        private static void IdiotAnnouncer()
        {
            System.Console.WriteLine("Well, you are extremely inteligent human.");
        }

        private static void RunExtraExperiments(string DbSize)
        {
            var cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DbSize + "M.mdf") + ";Integrated Security=True";
            var dbSizeNumber = Convert.ToDouble(DbSize);
            var memorySizes = new double[] { dbSizeNumber * 0.1, dbSizeNumber * 0.5, dbSizeNumber, dbSizeNumber * 2 };
            var entriesCounts = new int[] { 1000, 10000, 100000};

            foreach (var memorySize in memorySizes)
            {
                foreach (var entriesCount in entriesCounts)
                {
                    System.Console.WriteLine(String.Format("Starting test with limited memory size to: {0} and cache entries to: {1}", memorySize, entriesCount));
                    var results = RunExtraExperiment(cs, typeof(Logic.Experiments02.Experiment01), new ExperimentSettings(memorySize, entriesCount, int.MaxValue, TimeSpan.MaxValue));
                    ExportResults(results, memorySize + "_" + entriesCount + "_" + DbSize);
                }
            }
        }

        private static void ExportResults(List<Logic.Experiments02.ExperimentResult> results, string nameUniquePart)
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameUniquePart + "_export_" + DateTime.Now.ToFileTime() + ".json");

            var startTime = results.First().Time;
            results.ForEach(r =>
            {
                r.MemorySize = Math.Round(r.MemorySize, 2);
                var time = (r.Time - startTime);
                r.FormattedTime = String.Format("{0}:{1}.{2}", time.Minutes, time.Seconds, time.Milliseconds);
            });

            var exportCol = new List<Logic.Experiments02.ExperimentResult>(results);

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, exportCol);
            }
        }

        private static void RunBasicExperiments()
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
        }

        public static List<ExperimentResult> TodayTomorrowToyota(string connectionString, Type experimentType)
        {
            return new ExperimentService().Start((ExperimentBase)Activator.CreateInstance(experimentType, connectionString));
        }

        public static List<Logic.Experiments02.ExperimentResult> RunExtraExperiment(string connectionString, Type experimentType, ExperimentSettings settings)
        {
            return new ExperimentService().StartExtra((Logic.Experiments02.ExperimentBase)Activator.CreateInstance(experimentType, connectionString,settings));
        }
    }
}
