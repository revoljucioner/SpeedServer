using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SpeedServerApi.Models;
using Newtonsoft.Json;
//using System.Web.Script.Serialization.JavaScriptSerializer;

namespace Speed_Server.Controllers
{
    public static class GoogleAPI
    {
        private static string GoogleRoadsAPIKey { get; } = "AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM";
        private static string url { get; } = "https://roads.googleapis.com/v1/snapToRoads?path=";
        private static bool interpolate { get; } = true;
        private static HttpClient client = new HttpClient();
        public static SpeedModel GetFullSpeedModel(Location[] locations)
        {
            List<string> locationsList = new List<string>();

            foreach (var el in locations)
            {
                locationsList.Add(el.ToString());
            }

            string locationsString = String.Join("|", locationsList);

            string urlRequest = url + locationsString + "&interpolate=" + interpolate + "&key=" + GoogleRoadsAPIKey;

            WebRequest request = WebRequest.Create(urlRequest);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            string responseFromServer = reader.ReadToEnd();

            response.Close();

            SpeedModel speedModel = JsonConvert.DeserializeObject<SpeedModel>(responseFromServer);

            return speedModel;
        }

    }
}
