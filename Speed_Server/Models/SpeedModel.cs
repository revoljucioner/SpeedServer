using System.Collections.Generic;
using Speed_Server.Models;

namespace SpeedServerApi.Models
{
    public class SpeedModel
    {
        public SnappedPoint[] snappedPoints { get; set; }

        public SpeedModel()
        {
        }

        public SpeedModel(List<SpeedModel> speedModelList)
        {
            List<SnappedPoint> snappedPointsList = new List<SnappedPoint>();

            foreach (var speedModel in speedModelList)
            {
                snappedPointsList.AddRange(speedModel.snappedPoints);
            }

            this.snappedPoints = snappedPointsList.ToArray();
        }
    }
}