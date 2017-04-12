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

        private static int[] _dbs = {2, 4, 8, 10, 20};

        private static List<Type> _experiments = new List<Type>
        {
            //new ExperimentService(new Experiment01(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment02(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment03(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment04(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment05(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment06(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment07(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment08(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment09(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment10(ConnectionString200)).Start(),
                //new ExperimentService(new Experiment13(ConnectionString200)).Start(),
                typeof(Experiment14)
        };
        public static void Main(string[] args)
        {
            var experiments = new List<List<ExperimentResult>>();
            foreach (var db in _dbs)
            {
                foreach (var experiment in _experiments)
                {

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, db + "00M.mdf");
                    string copyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, db + "00M_cpy.mdf");
                    string copyConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, db + "00M_cpy.mdf") + ";Integrated Security=True";
                    File.Copy(path, copyPath, true);
                    experiments.Add(TodayTomorrowToyota(copyConnectionString, experiment));
                    File.Delete(copyPath);
                }

                for (int i = 0; i < experiments.Count; i++)
                {
                    System.Console.WriteLine("Db size: " + db + "Experiment " + (i + 1));
                    for (int j = 0; j < experiments[i].Count; j++)
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
