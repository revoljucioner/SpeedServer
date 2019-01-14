using Newtonsoft.Json;

namespace SpeedServer.Models
{
    public class LocationWithElevation: Location
    {
        public double elevation { get; set; }

        [JsonConstructor]
        public LocationWithElevation()
        { }

        public LocationWithElevation(Location location)
        {
            this.latitude = location.latitude;
            this.longitude = location.longitude;
        }

        public LocationWithElevation(Location location, double _elevation) : base(location.latitude, location.longitude)
        {
            elevation = _elevation;
        }
    }
}
