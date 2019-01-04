using SpeedServer.Models;

namespace Speed_Server.Models
{
    public class SnappedPointRoad
    {
        public Location Location { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }

        public SnappedPointRoad(Location location)
        {
            this.Location = location;
        }

        public SnappedPointResponse ToSnappedPointResponse()
        {
            var snappedPointResponse = new SnappedPointResponse();
            snappedPointResponse.Location = new LocationWithElevation(Location);
            snappedPointResponse.originalIndex = originalIndex;
            snappedPointResponse.placeId = placeId;
            return snappedPointResponse;
        }
    }
}
