using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeedServerApi.Models;

namespace SpeedServerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SpeedServer")]
    public class SpeedServerController : Controller
    {
        private readonly SpeedServerContext _context;

        public SpeedServerController(SpeedServerContext context)
        {
            _context = context;

            if (_context.SpeedModels.Count() == 0)
            {
                _context.SpeedModels.Add(new SpeedModel { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] SpeedModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            //_context.SpeedModels.Add(item);
            //_context.SaveChanges();

            //return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
            //
            item.latitude = 5;
            return new ObjectResult(item);
        }
    }
}