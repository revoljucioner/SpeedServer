﻿using System;
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
            limitPointPerQuery = 3;
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
            LocationTime[] locationTimeArray = speedModel.snappedPoints.Select(i => i.Location).ToArray();

            var groupedLocationTimeByRequest = GroupLocationTimeArrayByQuery(locationTimeArray);

            List<GoogleElevationResponse> googleElevationResponseList = ExecuteAllRequest(groupedLocationTimeByRequest);

            var compliteGoogleElevationResponse = new GoogleElevationResponse(googleElevationResponseList);
            speedModel.FillElevation(compliteGoogleElevationResponse);

            return speedModel;
        }

        private string MadeUrlRequest(IGrouping<int, Location> locations)
        {
            string locationsString = String.Join("|", locations);
            string urlRequest = String.Format(urlApi, locationsString, GoogleApiKey);
            return urlRequest;
        }

        private List<GoogleElevationResponse> ExecuteAllRequest(IGrouping<int, LocationTime>[] groupedLocationTimeByRequest)
        {
            List<GoogleElevationResponse> speedModelList = new List<GoogleElevationResponse>();

            for (var i = 0; i < groupedLocationTimeByRequest.Length; i++)
            {
                var urlRequest = MadeUrlRequest(groupedLocationTimeByRequest[i]);
                var responseFromServer = ExecuteQuery(urlRequest);
                //
                responseFromServer.Replace("lat", "latitude").Replace("lng", "longitude");
                //
                GoogleElevationResponse speedModel = JsonConvert.DeserializeObject<GoogleElevationResponse>(responseFromServer);                
                speedModelList.Add(speedModel);
            }
            return speedModelList;
        }
    }
}
