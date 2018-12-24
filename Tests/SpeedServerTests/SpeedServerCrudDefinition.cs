using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace Tests.SpeedServerTests
{
    public class SpeedServerCrudDefinition: BaseDefinition
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SpeedServerApiEmptyTrackBadRequest()
        {
            WebRequest request = WebRequest.Create(urlRequest);
            WebResponse response = request.GetResponse();
        }
    }
}