using System;

namespace Speed_Server.Models
{
    public class SnappedPointRequest
    {
        public Location Location { get; set; }
        public DateTime time { get; set; }

        public SnappedPointRequest(double latitude, double longitude, DateTime _time)
        {
            var location = new Location{latitude = latitude, longitude = longitude};
            Location = location;
            time = _time;
        }
    }
}
