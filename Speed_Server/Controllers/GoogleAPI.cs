using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SpeedServerApi.Models;

namespace Speed_Server.Controllers
{
    public static class GoogleAPI
    {
        private static string GoogleRoadsAPIKey { get; set; } = "AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM";
        private static string url { get; set; } = "https://roads.googleapis.com/v1/snapToRoads?path=";
        private static HttpClient client = new HttpClient();
        public static SpeedModel GetFullSpeedModel(Location[] locations)
        {


            var url = CreateProductAsync("https://roads.googleapis.com/v1/snapToRoads?path=-35.27801,149.12958|-35.28032,149.12907|-35.28099,149.12929|-35.28144,149.12984|-35.28194,149.13003|-35.28282,149.12956|-35.28302,149.12881|-35.28473,149.12836&interpolate=true&key=AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM");
            //
            //API KEY GOOGLE ROADS: AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM
            //
            return new SpeedModel();
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
