using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Speed_Server.Models;

namespace Tests.Steps
{
    //[Route("api/SpeedServer")]
    public class SpeedServerSteps: BaseSteps
    {
        public SpeedServerSteps()
        {
            Endpoint = "api/SpeedServer";
        }

        public async Task<HttpResponseMessage> PostSpeedServerApiGetResponse(string requestStringContent)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(requestStringContent,
                    Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<SpeedModel> PostSpeedServerApiAndGetSpeedModel(string requestStringContent)
        {
            var response = await PostSpeedServerApiGetResponse(requestStringContent);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content.ToString());

            var contentString = await response.Content.ReadAsStringAsync();
            SpeedModel value;
            try
            {
                value = JsonConvert.DeserializeObject<SpeedModel>(contentString);
            }
            catch (Exception exception)
            {
                throw new Exception("Server return wrong data");
            }
            return value;
        }
    }
}
