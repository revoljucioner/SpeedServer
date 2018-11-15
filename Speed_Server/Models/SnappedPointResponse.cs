using System;

namespace Speed_Server.Models
{
    public class SnappedPointResponse
    {
        public LocationWithElevation Location { get; set; }
        public DateTime time { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }

        public SnappedPointResponse()
        {
        }

        public SnappedPointResponse(SnappedPointRequest snappedPointRequest)
        {
            this.Location = new LocationWithElevation(snappedPointRequest.Location);
            this.time = snappedPointRequest.time;
        }

        public SnappedPointResponse(SnappedPointRoad snappedPointRoad)
        {
            this.Location = new LocationWithElevation(snappedPointRoad.Location);
            this.originalIndex = snappedPointRoad.originalIndex;
            this.placeId = snappedPointRoad.placeId;

        }
        public SnappedPointResponse(SnappedPointElevation snappedPointElevation)
        {
            LocationWithElevation locationElevation = new LocationWithElevation{ latitude = snappedPointElevation.Location.latitude, longitude = snappedPointElevation.Location.longitude , elevation = snappedPointElevation.Elevation};
            this.Location = locationElevation;
        }
    }
}
