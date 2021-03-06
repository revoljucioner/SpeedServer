﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SpeedServer.Core.Extensions;
using SpeedServer.Core.Models;
using SpeedServer.Models;

namespace SpeedServer.Core.Controllers.GoogleAPIs
{
    public class GoogleRoadsApi : GoogleApi
    {
        public GoogleRoadsApi()
        {
            limitPointPerQuery = 100;
            GoogleApiKey = "AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM";
            urlApi = "https://roads.googleapis.com/v1/snapToRoads?path={0}&interpolate={1}&key={2}";
        }

        public GoogleRoadsApi(int limitPointPerQuery)
        {
            this.limitPointPerQuery = limitPointPerQuery;
            GoogleApiKey = "AIzaSyDWhsEa4PkUPwfxQIpMPsPid0rmPXYFdPM";
            urlApi = "https://roads.googleapis.com/v1/snapToRoads?path={0}&interpolate={1}&key={2}";
        }

        public SpeedModel FillSpeedModel(SpeedModel speedModel, bool interpolate)
        {
            //var locationTime = speedModel.snappedPoints.Select(x => new { x.Location, x.time }).ToArray();

            LocationTime[] locationTimeArray = PullLocationTimeArrayFromSpeedModel(speedModel);

            var groupedLocationTimeByRequest = GroupLocationTimeArrayByQuery(locationTimeArray);

            List<SpeedModel> speedModelList = ExecuteAllRequest(groupedLocationTimeByRequest, interpolate);

            var compliteSpeedModel = new SpeedModel(speedModelList);

            return compliteSpeedModel;
        }

        private string MadeUrlRequest(IGrouping<int, Location> locations, bool interpolate)
        {
            string locationsString = String.Join("|", locations);
            string urlRequest = String.Format(urlApi, locationsString, interpolate, GoogleApiKey);
            return urlRequest;
        }

        private List<SpeedModel> ExecuteAllRequest(IGrouping<int, LocationTime>[] groupedLocationTimeByRequest, bool interpolate)
        {
            List<SpeedModel> speedModelList = new List<SpeedModel>();

            for (var i = 0; i < groupedLocationTimeByRequest.Length; i++)
            {
                var urlRequest = MadeUrlRequest(groupedLocationTimeByRequest[i], interpolate);
                var responseFromServer = ExecuteQuery(urlRequest);
                GoogleRoadResponse googleRoadResponse = JsonConvert.DeserializeObject<GoogleRoadResponse>(responseFromServer);

                //SpeedModel speedModel = new SpeedModel(googleRoadResponse.snappedPoints);
                var speedModel = SpeedModelExtensions.FromSnappedPointRoadArray(googleRoadResponse.snappedPoints);

                int previousOriginalElementIndex = 0;

                var groupOfTimeArray = groupedLocationTimeByRequest[i].ToArray();
                speedModel.snappedPoints[0].time = groupOfTimeArray[0].time;

                for (var j = 1; j < speedModel.snappedPoints.Length; j++)
                {
                    if (speedModel.snappedPoints[j].originalIndex != 0)
                    {
                        var indexDifferenceBetweenOriginalElements = j - previousOriginalElementIndex;
                        var timeDifferenceBetweenOriginalElements =
                            groupOfTimeArray[speedModel.snappedPoints[j].originalIndex].time - groupOfTimeArray[speedModel.snappedPoints[previousOriginalElementIndex].originalIndex].time;
                        var timeDifferenceBetweenNeighborElements = timeDifferenceBetweenOriginalElements.TotalMilliseconds / indexDifferenceBetweenOriginalElements;
                        var nextOriginalElementTime = groupOfTimeArray[speedModel.snappedPoints[j].originalIndex].time;
                        int schetchik = 0;

                        for (var k = j; k > previousOriginalElementIndex; k--)
                        {
                            speedModel.snappedPoints[k].time =
                                nextOriginalElementTime.AddMilliseconds(-1 * schetchik * timeDifferenceBetweenNeighborElements);
                            schetchik = schetchik + 1;
                        }
                        previousOriginalElementIndex = j;
                    }
                }
                speedModelList.Add(speedModel);
            }
            return speedModelList;
        }
    }
}
