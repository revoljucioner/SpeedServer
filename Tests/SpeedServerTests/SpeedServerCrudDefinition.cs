using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Speed_Server;
using Speed_Server.Controllers;

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
            var g = new GoogleEvaluationApi();
            var t2 = Speed_Server.Configuration.BaseUrl;
            WebRequest request = WebRequest.Create("sdsd");
            WebResponse response = request.GetResponse();
        }
    }
}