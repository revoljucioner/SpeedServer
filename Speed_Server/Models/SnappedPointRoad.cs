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
    }
}
