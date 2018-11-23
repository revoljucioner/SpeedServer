using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedServerApi.Models;
using Speed_Server.Models;

namespace Speed_Server.Controllers
{
    [Produces("application/json")]
    [Route("api/SpeedServer")]
    public class SpeedServerController : Controller
    {
        private readonly SpeedServerContext _context;
        private static HttpClient client = new HttpClient();
        private readonly GoogleRoadsApi _googleRoadsApi;
        private readonly GoogleEvaluationApi _googleEvaluationApi;
        private readonly IDbController _dbController;
        public SpeedServerController(SpeedServerContext context)
        {
            _googleRoadsApi = new GoogleRoadsApi();
            _googleEvaluationApi = new GoogleEvaluationApi();
            _dbController = new MsSqlController();
            //_context = context;

            //if (_context.SpeedModels.Count() == 0)
            //{
            //    _context.SpeedModels.Add(new SpeedModel { Name = "Item1" });
            //    _context.SaveChanges();
            //}
        }

        [HttpPost]
        public IActionResult Create([FromBody] SnappedPointRequest[] snappedPointsRequests)
        {
            if (snappedPointsRequests == null)
            {
                return BadRequest();
                //return StatusCode(418);
            }
            //try
            //{
            SpeedModel speedModel = new SpeedModel(snappedPointsRequests);

            SpeedModel speedModelWithRoads = _googleRoadsApi.FillSpeedModel(speedModel, false);
            SpeedModel speedModelWithRoadsAndEvaluations = _googleEvaluationApi.FillSpeedModel(speedModelWithRoads);
            //fullSpeedModel.RemoveExtraPoints();
            return new ObjectResult(speedModelWithRoadsAndEvaluations);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest();
            //}       
            try
            {
                _dbController.SaveSnappedPointRequestsToDb(snappedPointsRequests);
            }
            catch (Exception e)
            {
            }
        }
    }
}