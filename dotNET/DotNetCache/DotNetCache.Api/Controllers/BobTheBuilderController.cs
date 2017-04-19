using System;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ExperimentInfo Experiment(int id)
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
                case 6:
                    ExperimentService = new ExperimentService(new Experiment06(ConnectionString));
                    break;
                case 7:
                    ExperimentService = new ExperimentService(new Experiment07(ConnectionString));
                    break;
                case 8:
                    ExperimentService = new ExperimentService(new Experiment08(ConnectionString));
                    break;
                case 9:
                    ExperimentService = new ExperimentService(new Experiment09(ConnectionString));
                    break;
                case 10:
                    ExperimentService = new ExperimentService(new Experiment10(ConnectionString));
                    break;
                case 11:
                    ExperimentService = new ExperimentService(new Experiment11(ConnectionString));
                    break;
                case 12:
                    ExperimentService = new ExperimentService(new Experiment12(ConnectionString));
                    break;
                case 13:
                    ExperimentService = new ExperimentService(new Experiment13(ConnectionString));
                    break;
                case 14:
                    ExperimentService = new ExperimentService(new Experiment14(ConnectionString));
                    break;
                default:
                    throw new ArgumentException("Experiment does not exist or is not registered");
            }
            var result = ExperimentService.Start();
            //return result.Select(x => x.ToString()).Aggregate((st, nd) => $"{st}\n{nd}");
            return new ExperimentInfo { ExperimentId = id, Results = result };
        }
    }
}
