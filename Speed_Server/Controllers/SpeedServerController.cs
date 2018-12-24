using System;
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
        private readonly GoogleRoadsApi _googleRoadsApi;
        private readonly GoogleEvaluationApi _googleEvaluationApi;
        public SpeedServerController(SpeedServerContext context)
        {
            _googleRoadsApi = new GoogleRoadsApi();
            _googleEvaluationApi = new GoogleEvaluationApi();
        }

        [HttpPost]
        public IActionResult Create([FromBody] SnappedPointRequest[] snappedPointsRequests)
        {
            if (snappedPointsRequests == null)
            {
                return BadRequest("Track is empty");
            }
            SpeedModel speedModel = new SpeedModel(snappedPointsRequests);
            SpeedModel speedModelWithRoadsAndEvaluations;

            try
            {
                SpeedModel speedModelWithRoads = _googleRoadsApi.FillSpeedModel(speedModel, false);
                speedModelWithRoadsAndEvaluations = _googleEvaluationApi.FillSpeedModel(speedModelWithRoads);
            }
            catch (Exception e)
            {
                return BadRequest("Remote server error, please try again later.");
            }

            return new ObjectResult(speedModelWithRoadsAndEvaluations);             
        }
    }
}