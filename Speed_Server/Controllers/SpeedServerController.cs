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

namespace SpeedServerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SpeedServer")]
    public class SpeedServerController : Controller
    {
        private readonly SpeedServerContext _context;
        private static HttpClient client = new HttpClient();
        public SpeedServerController(SpeedServerContext context)
        {
            //_context = context;

            //if (_context.SpeedModels.Count() == 0)
            //{
            //    _context.SpeedModels.Add(new SpeedModel { Name = "Item1" });
            //    _context.SaveChanges();
            //}
        }

        //http://localhost:57929/api/SpeedServer
        // input format of json
        //    [
        //    { 
        //        "latitude": -35.280734599999995,
        //        "longitude": 149.1291517


        //    },
        //    {
        //        "latitude": -35.2807852,
        //        "longitude": 149.1291716

        //    }
        //]
        //
        [HttpPost]
        public IActionResult Create([FromBody] Location[] locations)
        {
            if (locations == null)
            {
                return BadRequest();
            }

            SpeedModel fullSpeedModel = GoogleAPI.GetFullSpeedModel(locations);

            return new ObjectResult(fullSpeedModel);
        }
    }
}