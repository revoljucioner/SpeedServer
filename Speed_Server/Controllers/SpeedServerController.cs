﻿using System;
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
        //[
        //    { 
        //        "latitude": -35.2807341,
        //        "longitude": 149.1291511,
        //        "time": "2018-02-18T03:36:18.1036604+02:00"

        //   },
        //    {
        //       "latitude": -35.2807342,
        //        "longitude": 149.1291512

        //   },
        //               {
        //       "latitude": -35.2807343,
        //        "longitude": 149.1291513

        //   },
        //               {
        //       "latitude": -35.2807344,
        //        "longitude": 149.1291514

        //   },
        //               {
        //       "latitude": -35.280736,
        //        "longitude": 149.1293

        //   }
        //]
        //
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
                SpeedModel fullSpeedModel = GoogleAPI.GetFullSpeedModel(locations, true);
                return new ObjectResult(fullSpeedModel);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest();
            //}         
        }
    }
}