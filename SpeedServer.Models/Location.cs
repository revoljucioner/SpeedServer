using System;

namespace SpeedServer.Models
{
    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Location()
        {
        }

        public Location(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        // needed for making urlRequest for googleApi's
        public override string ToString()
        {
            string str = latitude + "," + longitude;
            return str;
        }

        public void RoundCoordinats(int decimals)
        {
            latitude = Math.Round(latitude, decimals);
            longitude = Math.Round(longitude, decimals);
        }
    }
}
