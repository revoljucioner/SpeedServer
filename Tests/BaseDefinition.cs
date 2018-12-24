using System.Net.Http;

namespace Tests
{
    public class BaseDefinition
    {
        protected HttpClient Client;

        public BaseDefinition()
        {
            Client = new HttpClient();
        }
    }
}