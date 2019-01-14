using System.Collections.Generic;
using SpeedServer.Core.Models;
using SpeedServer.Models;

namespace SpeedServer.Core.Extensions
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
