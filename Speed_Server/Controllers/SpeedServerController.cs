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
        public SpeedServerController(SpeedServerContext context)
        {
            _googleRoadsApi = new GoogleRoadsApi();
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
            
                SpeedModel fullSpeedModel = _googleRoadsApi.FillSpeedModel(locations, true);
            //fullSpeedModel.RemoveExtraPoints();
                return new ObjectResult(fullSpeedModel);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest();
            //}         
        }
    }
}