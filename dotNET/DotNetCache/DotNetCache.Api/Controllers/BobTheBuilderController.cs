using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using DotNetCache.Api.Models;
using DotNetCache.Logic.Experiments;
using DotNetCache.Logic.Services;

namespace DotNetCache.Api.Controllers
{
    public class BobTheBuilderController : ApiController
    {
        public ExperimentService ExperimentService;
        public static string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "200M.mdf") + ";Integrated Security=True";

        [HttpGet]
        [Route("api/BobTheBuilder/Experiment/{id}")]
        public string /*ExperimentInfo*/ Experiment(int id)
        {
            switch (id)
            {
                case 1:
                    ExperimentService = new ExperimentService(new Experiment01(ConnectionString));
                    break;
                case 2:
                    ExperimentService = new ExperimentService(new Experiment02(ConnectionString));
                    break;
                case 3:
                    ExperimentService = new ExperimentService(new Experiment03(ConnectionString));
                    break;
                case 4:
                    ExperimentService = new ExperimentService(new Experiment04(ConnectionString));
                    break;
                case 5:
                    ExperimentService = new ExperimentService(new Experiment05(ConnectionString));
                    break;
                default:
                    throw new ArgumentException("Experiment does not exist or is not registered");
            }
            var result = ExperimentService.Start();
            return result.Select(x => x.ToString()).Aggregate((st, nd) => $"{st}\n{nd}");
        }
    }
}
