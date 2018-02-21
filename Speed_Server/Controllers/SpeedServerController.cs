using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedServerApi.Models;
using Speed_Server.Controllers;
using Newtonsoft.Json;
using Speed_Server.Models;

namespace SpeedServerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SpeedServer")]
    public class SpeedServerController : Controller
    {
        private readonly SpeedServerContext _context;
        private static HttpClient client = new HttpClient();
        private readonly GoogleRoadsApi _googleRoadsApi;
        private readonly GoogleEvaluationApi _googleEvaluationApi;
        public SpeedServerController(SpeedServerContext context)
        {
            _googleRoadsApi = new GoogleRoadsApi();
            _googleEvaluationApi = new GoogleEvaluationApi();
            //_context = context;

            //if (_context.SpeedModels.Count() == 0)
            //{
            //    _context.SpeedModels.Add(new SpeedModel { Name = "Item1" });
            //    _context.SaveChanges();
            //}
        }

        [HttpPost]
        public IActionResult Create([FromBody] LocationTime[] locations)
        {
            if (locations == null)
            {
                return BadRequest();
                //return StatusCode(418);
            }
            //try
            //{
            SpeedModel speedModel = new SpeedModel(locations);

            SpeedModel speedModelWithRoads = _googleRoadsApi.FillSpeedModel(speedModel, true);
            SpeedModel speedModelWithRoadsAndEvaluations = _googleEvaluationApi.FillSpeedModel(speedModelWithRoads);
            //fullSpeedModel.RemoveExtraPoints();
            return new ObjectResult(speedModelWithRoadsAndEvaluations);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest();
            //}         
        }
    }
}