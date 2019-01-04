using System.Collections.Generic;
using SpeedServer.Models;
using Speed_Server.Models;

namespace Speed_Server.Extensions
{
    public static class SpeedModelExtensions
    {
        public static SpeedModel FromSnappedPointRoadArray(SnappedPointRoad[] snappedPointsRoad)
        {
            var speedModel = new SpeedModel();
            var snappedPointsRoadList = new List<SnappedPointResponse>();

            foreach (var snappedPointRoad in snappedPointsRoad)
            {
                snappedPointsRoadList.Add(snappedPointRoad.ToSnappedPointResponse());
            }

            speedModel.snappedPoints = snappedPointsRoadList.ToArray();
            return speedModel;
        }

        public static SpeedModel FromSnappedPointElevationArray(SnappedPointElevation[] snappedPointsElevation)
        {
            var speedModel = new SpeedModel();
            var snappedPointsRoadList = new List<SnappedPointResponse>();

            foreach (var snappedPointElevation in snappedPointsElevation)
            {
                snappedPointsRoadList.Add(snappedPointElevation.ToSnappedPointResponse());
            }

            speedModel.snappedPoints = snappedPointsRoadList.ToArray();
            return speedModel;
        }
    }
}
