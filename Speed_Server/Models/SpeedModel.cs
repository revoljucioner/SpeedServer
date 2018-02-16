using System.Collections.Generic;
using Speed_Server.Models;

namespace SpeedServerApi.Models
{
    public class SpeedModel
    {
        public SnappedPoint[] snappedPoints { get; set; }

        public SpeedModel(List<SpeedModel> speedModelList)
        {
            //TODO

        }
    }
}