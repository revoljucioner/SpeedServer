﻿using System;

namespace SpeedServer.Models
{
    public class LocationWithElevation: Location
    {
        public double elevation { get; set; }

        public LocationWithElevation()
        { }

        public LocationWithElevation(Location location)
        {
            this.latitude = location.latitude;
            this.longitude = location.longitude;
        }
    }
}
