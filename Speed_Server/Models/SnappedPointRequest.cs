using System;

namespace Speed_Server.Models
{
    public class SnappedPointRequest
    {
        public Location Location { get; set; }
        public DateTime time { get; set; }

        public SnappedPointRequest()
        { }

        public SnappedPointRequest(SnappedPointRequest snappedPointRequest)
        {
            this.Location = snappedPointRequest.Location;
        }
    }
}
