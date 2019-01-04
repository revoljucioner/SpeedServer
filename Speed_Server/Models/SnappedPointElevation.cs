using SpeedServer.Models;

namespace Speed_Server.Models
{
    public class SnappedPointElevation
    {
        public Location Location { get; set; }
        public double Elevation { get; set; }
        public double Resolution { get; set; }

        public SnappedPointResponse ToSnappedPointResponse()
        {
            var snappedPointResponse = new SnappedPointResponse();
            snappedPointResponse.Location = new LocationWithElevation
            {
                latitude = Location.latitude,
                longitude = Location.longitude,
                elevation = Elevation
            };
            return snappedPointResponse;
        }
    }
}
