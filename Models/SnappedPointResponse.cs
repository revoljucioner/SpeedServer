using System;

namespace Models
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
    }
}
