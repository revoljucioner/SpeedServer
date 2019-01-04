using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpeedServer.Models;

namespace Tests.Services
{
    public class SpeedServerService: BaseService
    {
        protected override string Endpoint => "api/SpeedServer";

        public async Task<HttpResponseMessage> PostSpeedServerApiGetResponse(IEnumerable<SnappedPointRequest> snappedPointsRequestArray)
        {
            var content = JsonConvert.SerializeObject(snappedPointsRequestArray);
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(content,
                    Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<SpeedModel> PostSpeedServerApiAndGetSpeedModel(IEnumerable<SnappedPointRequest> snappedPointsRequestArray)
        {
            var response = await PostSpeedServerApiGetResponse(snappedPointsRequestArray);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content.ToString());

            var contentString = await response.Content.ReadAsStringAsync();
            SpeedModel value;
            try
            {
                value = JsonConvert.DeserializeObject<SpeedModel>(contentString);
            }
            catch (Exception)
            {
                throw new Exception("Server return wrong data");
            }
            return value;
        }
    }
}
