using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpeedServer.Core.Controllers.GoogleAPIs;
using SpeedServer.Models;

namespace SpeedServer.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/SpeedServer")]
    //[Route("api/[controller]")]
    public class SpeedServerController : Controller
    {
        private readonly GoogleRoadsApi _googleRoadsApi;
        private readonly GoogleEvaluationApi _googleEvaluationApi;
        public SpeedServerController()
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

            if (snappedPointsRequests.Any(i => i.Location ==null))
                return BadRequest("Location cannot be null");
            if (snappedPointsRequests.Any(i => i.Location.latitude.Equals(0) || i.Location.longitude.Equals(0)))
                return BadRequest("Latitude and Longitude cannot be 0");

            var speedModel = new SpeedModel(snappedPointsRequests);

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