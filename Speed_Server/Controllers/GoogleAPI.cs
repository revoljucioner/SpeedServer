using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SpeedServerApi.Models;
using Newtonsoft.Json;

namespace Speed_Server.Controllers
{
    public static class GoogleAPI
    {
        private static string GoogleRoadsAPIKey { get; } = "AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM";
        private static string urlRoadsAPI { get; } = "https://roads.googleapis.com/v1/snapToRoads?path={0}&interpolate={1}&key={2}";
        private static HttpClient client = new HttpClient();
        public static SpeedModel GetFullSpeedModel(Location[] locations, bool interpolate)
        {
            List<string> locationsList = new List<string>();

            foreach (var el in locations)
            {
                locationsList.Add(el.ToString());
            }

            string locationsString = String.Join("|", locationsList);

            string urlRequest = String.Format(urlRoadsAPI, locationsString, interpolate, GoogleRoadsAPIKey);

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
