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
    public abstract class GoogleApi
    {
        protected int limitPointPerQuery { get; set; }
        protected string GoogleApiKey { get; set; }
        protected string urlApi { get; set; }

        protected string ExecuteQuery(string urlRequest)
        {
            WebRequest request = WebRequest.Create(urlRequest);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            string responseFromServer = reader.ReadToEnd();

            response.Close();

            return responseFromServer;
        }

        protected IGrouping<int, LocationTime>[] GroupLocationTimeArrayByQuery(LocationTime[] locations)
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
