using System.Net.Http;
using Tests.Environment;

namespace Tests.Services
{
    public abstract class BaseService
    {
        protected virtual string Endpoint { get; }
        protected string Url => string.Concat(App.Configuration.Environment.BaseUrl, Endpoint);

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
