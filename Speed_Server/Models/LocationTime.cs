using System;
using SpeedServer.Models;

namespace Speed_Server.Models
{
    public class LocationTime: Location
    {
        public DateTime time { get; set; }

        public LocationTime(Location location, DateTime time)
        {
            this.latitude = location.latitude;
            this.longitude = location.longitude;
            this.time = time;
        }
    }
}
