using DotNetCache.Logic.Experiments;
using DotNetCache.Logic.Services;

namespace DotNetCache.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new ExperimentService(new Experiment01());
            System.Console.WriteLine(service.Start());
        }
    }
}
