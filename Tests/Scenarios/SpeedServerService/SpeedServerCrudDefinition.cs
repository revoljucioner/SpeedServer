using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.TestData;

namespace Tests.Scenarios.SpeedServerService
{
    public class SpeedServerCrudDefinition: BaseDefinition
    {
        [Test]
        public async Task SpeedServerApiValidTrackStatusOk()
        {
            var speedServerSteps = new Services.SpeedServerService();
            var httpResponseMessage = await speedServerSteps.PostSpeedServerApiGetResponse(TracksStorage.TestSnappedPointRequestArray);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task SpeedServerApiEmptyTrackStatusBadRequest()
        {
            var speedServerSteps = new Services.SpeedServerService();
            var httpResponseMessage = await speedServerSteps.PostSpeedServerApiGetResponse(null);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}