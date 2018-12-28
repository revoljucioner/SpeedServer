using System.Net.Http;
using Tests.Environment;

namespace Tests.Steps
{
    public abstract class BaseSteps
    {
        protected static string Endpoint;
        protected string Url = string.Concat(App.Configuration.Environment.BaseUrl, Endpoint);

        private static HttpClient _client;

        protected HttpClient GetClient()
        {
            if (_client != null)
                return _client;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            return _client;
        }
    }
}
