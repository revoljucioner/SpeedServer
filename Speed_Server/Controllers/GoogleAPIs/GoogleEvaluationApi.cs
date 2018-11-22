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
    public class GoogleEvaluationApi : GoogleApi
    {
        public GoogleEvaluationApi()
        {
            limitPointPerQuery = 512;
            GoogleApiKey = "AIzaSyC2FZl6lpRYmLqLPB6py-fd_Q9Q6C6AIiQ";
            urlApi = "https://maps.googleapis.com/maps/api/elevation/json?locations={0}&key={1}";
        }

        public GoogleEvaluationApi(int limitPointPerQuery)
        {
            this.limitPointPerQuery = limitPointPerQuery;
            GoogleApiKey = "AIzaSyC2FZl6lpRYmLqLPB6py-fd_Q9Q6C6AIiQ";
            urlApi = "https://maps.googleapis.com/maps/api/elevation/json?locations={0}&key={1}";
        }

        public SpeedModel FillSpeedModel(SpeedModel speedModel)
        {
            //var locationTime = speedModel.snappedPoints.Select(x => new { x.Location, x.time }).ToArray();

            LocationTime[] locationTimeArray = PullLocationTimeArrayFromSpeedModel(speedModel);

            var groupedLocationTimeByRequest = GroupLocationTimeArrayByQuery(locationTimeArray);

            List<SpeedModel> speedModelList = ExecuteAllRequest(groupedLocationTimeByRequest);

            //var compliteSpeedModel = new SpeedModel(speedModelList);
            var elevationSpeedModel = new SpeedModel(speedModelList);
            var compliteSpeedModel = new SpeedModel(speedModel, elevationSpeedModel);

            return compliteSpeedModel;
        }

        private string MadeUrlRequest(IGrouping<int, Location> locations)
        {
            string locationsString = String.Join("|", locations);
            string urlRequest = String.Format(urlApi, locationsString, GoogleApiKey);
            return urlRequest;
        }

        private List<SpeedModel> ExecuteAllRequest(IGrouping<int, LocationTime>[] groupedLocationTimeByRequest)
        {
            List<SpeedModel> speedModelList = new List<SpeedModel>();

            for (var i = 0; i < groupedLocationTimeByRequest.Length; i++)
            {
                var urlRequest = MadeUrlRequest(groupedLocationTimeByRequest[i]);
                var responseFromServer = ExecuteQuery(urlRequest);
                //
                var responseFromServerModelFrom = responseFromServer.Replace("lat", "latitude").Replace("lng", "longitude");
                //
                GoogleElevationResponse googleElevationResponse = JsonConvert.DeserializeObject<GoogleElevationResponse>(responseFromServerModelFrom);
                //foreach (var googleResponsePointElevation in googleElevationResponse.results)
                //{
                //    var speedModel = new SpeedModel(googleResponsePointElevation);
                //    speedModelList.Add(speedModel);
                //}
                var speedModel = new SpeedModel(googleElevationResponse.results);
                speedModelList.Add(speedModel);
            }
            return speedModelList;
        }
    }
}
