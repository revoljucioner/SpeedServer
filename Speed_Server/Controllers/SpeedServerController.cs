using System;
using System.Collections.Generic;
using System.Linq;
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


            //return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
            //
            //foreach (var snappedPoint in speedModel.snappedPoints)
            //{
            //    snappedPoint.originalIndex = 10;
            //}
            //
            SpeedModel fullSpeedModel = GoogleAPI.GetFullSpeedModel(locations);
            RunAsync().GetAwaiter().GetResult(); ;

            //
            return new ObjectResult(fullSpeedModel);
        }
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://roads.googleapis.com/v1/snapToRoads?path=-35.27801,149.12958|-35.28032,149.12907|-35.28099,149.12929|-35.28144,149.12984|-35.28194,149.13003|-35.28282,149.12956|-35.28302,149.12881|-35.28473,149.12836&interpolate=true&key=AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                //Product product = new Product
                //{
                //    Name = "Gizmo",
                //    Price = 100,
                //    Category = "Widgets"
                //};

                var url = CreateProductAsync("https://roads.googleapis.com/v1/snapToRoads?path=-35.27801,149.12958|-35.28032,149.12907|-35.28099,149.12929|-35.28144,149.12984|-35.28194,149.13003|-35.28282,149.12956|-35.28302,149.12881|-35.28473,149.12836&interpolate=true&key=AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM");
                Console.WriteLine($"Created at {url}");
                
                //// Get the product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                //// Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static async Task<HttpResponseMessage> CreateProductAsync(string yyu)
        {
            HttpResponseMessage response = await client.GetAsync(yyu);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response;
        }
    }
}