using System;

namespace Speed_Server.Models
{
    public class SnappedPointResponse
    {
        public LocationWithElevation Location { get; set; }
        public DateTime time { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }

        public SnappedPointResponse(Location location)
        {
            this.Location = (LocationWithElevation)location;
        }

        public SnappedPointResponse(SnappedPointRequest snappedPointRequest)
        {
            //this.Location = (LocationWithElevation)snappedPointRequest.Location;
            this.Location = new LocationWithElevation(snappedPointRequest.Location);
            this.time = snappedPointRequest.time;
        }

        public SnappedPointResponse(SnappedPointRoad snappedPointRoad)
        {
            //this.Location = (LocationWithElevation)snappedPointRoad.Location;
            this.Location = new LocationWithElevation(snappedPointRoad.Location);

        }
        public SnappedPointResponse(SnappedPointElevation snappedPointElevation)
        {
            //this.Location = (LocationWithElevation)snappedPointElevation.Location;
            LocationWithElevation locationElevation = new LocationWithElevation{ latitude = snappedPointElevation.Location.latitude, longitude = snappedPointElevation.Location.longitude , elevation = snappedPointElevation.Elevation};
            this.Location = locationElevation;
        }

        //public SnappedPointResponse(SnappedPointElevation snappedPointElevation, )
        //{
        //    Location locationElevation = new LocationWithElevation { latitude = snappedPointElevation.Location.latitude, longitude = snappedPointElevation.Location.longitude, elevation = snappedPointElevation.Elevation };
        //}

    }
}
