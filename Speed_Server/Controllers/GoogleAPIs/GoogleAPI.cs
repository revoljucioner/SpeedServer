using System.IO;
using System.Linq;
using System.Net;
using SpeedServer.Core.Models;
using SpeedServer.Models;

namespace SpeedServer.Core.Controllers.GoogleAPIs
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

        protected LocationTime[] PullLocationTimeArrayFromSpeedModel(SpeedModel speedModel)
        {
            LocationTime[] locationTimeArray = speedModel.snappedPoints.Select(x => new LocationTime(x.Location, x.time)).ToArray();
            return locationTimeArray;
        }
    }
}
