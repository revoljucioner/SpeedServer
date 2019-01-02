using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Tests.Environment;
using Tests.Helpers;
using Tests.Steps;

namespace Tests.SpeedServerTests
{
    public class SpeedServerCrudDefinition: BaseDefinition
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SpeedServerApiEmptyTrackStatusOk()
        {
            var speedServerSteps = new SpeedServerSteps();
            var httpResponseMessage = await speedServerSteps.PostSpeedServerApiGetResponse("[\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807341,\r\n      \"longitude\": 149.1291511\r\n    },\r\n    \"time\": \"2018-02-18T01:00:00.0000000+00:00\"\r\n  },\r\n  {\r\n    \"location\": {\r\n      \"latitude\": -35.2807342,\r\n      \"longitude\": 149.1291512\r\n    },\r\n    \"time\": \"2018-02-18T01:01:00.0000000+00:00\"\r\n  }]");
        }
    }
}