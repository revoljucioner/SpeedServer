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

        public static SpeedModel GetFullSpeedModel(LocationTime[] locationTimeArray, bool interpolate)
        {
            var groupedLocationByQuery = GroupLocationByQuery(locationTimeArray);
            var groupedTimeByQuery = GroupTimeByQuery(locationTimeArray);
            //
            List<SpeedModel> speedModelList = new List<SpeedModel>();

            for (var i = 0; i < groupedLocationByQuery.Length; i++)
            {
                var urlRequest = MadeUrlRequest(groupedLocationByQuery[i], interpolate);
                var responseFromServer = ExecuteQuery(urlRequest);
                SpeedModel speedModel = JsonConvert.DeserializeObject<SpeedModel>(responseFromServer);
                int predOriginalElementIndex = 0;
                for (var j = 1; j < speedModel.snappedPoints.Length; j++)
                {
                    var groupOfTimeArray = groupedTimeByQuery[i].ToArray();
                    speedModel.snappedPoints[0].Location.time = groupOfTimeArray[0];

                    if (speedModel.snappedPoints[j].originalIndex != 0)
                    {
                        var raznitsaMezdyOriginalnimiTochkamiVOtvete = j - predOriginalElementIndex;
                        var raznitsaVoVremeniMezdyOriginalnimiTochkami =
                            groupOfTimeArray[speedModel.snappedPoints[j].originalIndex] - groupOfTimeArray[speedModel.snappedPoints[predOriginalElementIndex].originalIndex];
                        var shagVMilisecMezdyTochkami = raznitsaVoVremeniMezdyOriginalnimiTochkami.TotalMilliseconds / raznitsaMezdyOriginalnimiTochkamiVOtvete;
                        var vremaVoVtoroyTochke = groupOfTimeArray[speedModel.snappedPoints[j].originalIndex];
                        int schetchik = 0;

                        for (var k = j ; k > predOriginalElementIndex; k--)
                        {
                            
                            speedModel.snappedPoints[k].Location.time =
                                vremaVoVtoroyTochke.AddMilliseconds(-1 * schetchik * shagVMilisecMezdyTochkami);
                            schetchik = schetchik + 1;
                        }
                        predOriginalElementIndex = j;
                    }
                }
                //}
                speedModelList.Add(speedModel);
            }
            //foreach (var locationGroup in groupedLocationByQuery)
            //{
            //    var urlRequest = MadeUrlRequest(locationGroup, interpolate);
            //    var responseFromServer = ExecuteQuery(urlRequest);
            //    SpeedModel speedModel = JsonConvert.DeserializeObject<SpeedModel>(responseFromServer);
            //    speedModelList.Add(speedModel);
            //}

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

        private static IGrouping<int, Location>[] GroupLocationByQuery(Location[] locations)
        {
            var groupedLocationByQuery = locations.Select((i, index) => new
            {
                i,
                index
            }).GroupBy(grouped => grouped.index / limitPointPerQuery, location => location.i).ToArray();
            return groupedLocationByQuery;
        }

        private static IGrouping<int, DateTime>[] GroupTimeByQuery(LocationTime[] locations)
        {
            var groupedTimeByQuery = locations.Select((i, index) => new
            {
                i,
                index
            }).GroupBy(grouped => grouped.index / limitPointPerQuery, location => location.i.time).ToArray();

            return groupedTimeByQuery;
        }
    }
}
