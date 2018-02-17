using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using SpeedServerApi.Models;
using Newtonsoft.Json;
using Speed_Server.Models;

namespace Speed_Server.Controllers
{
    public static class GoogleAPI
    {
        private const int limitPointPerQuery = 3;
        private static string GoogleRoadsAPIKey { get; } = "AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM";
        private static string urlRoadsAPI { get; } = "https://roads.googleapis.com/v1/snapToRoads?path={0}&interpolate={1}&key={2}";
        private static HttpClient client = new HttpClient();

        public static SpeedModel GetFullSpeedModel(Location[] locations, bool interpolate)
        {
            var groupedLocationByQuery = GroupLocationByQuery(locations);
            List<SpeedModel> speedModelList = new List<SpeedModel>();

            foreach (var locationGroup in groupedLocationByQuery)
            {
                var urlRequest = MadeUrlRequest(locationGroup, interpolate);
                var responseFromServer = ExecuteQuery(urlRequest);
                SpeedModel speedModel = JsonConvert.DeserializeObject<SpeedModel>(responseFromServer);
                speedModelList.Add(speedModel);
            }

            var compliteSpeedModel = new SpeedModel(speedModelList);

            return compliteSpeedModel;
        }

        private static string MadeUrlRequest(IGrouping<int, Location> locations, bool interpolate)
        {
            string locationsString = String.Join("|", locations);
            string urlRequest = String.Format(urlRoadsAPI, locationsString, interpolate, GoogleRoadsAPIKey);
            return urlRequest;
        }
  
        private static string ExecuteQuery(string urlRequest)
        {
            WebRequest request = WebRequest.Create(urlRequest);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            string responseFromServer = reader.ReadToEnd();

            response.Close();

            return responseFromServer;
        }

        private static IGrouping<int,Location>[] GroupLocationByQuery(Location[] locations)
        {
            var groupedLocationByQuery = locations.Select((i, index) => new
            {
                i,
                index
            }).GroupBy(grouped => grouped.index / limitPointPerQuery, location => location.i).ToArray();
            return groupedLocationByQuery;
        }
    }
}
