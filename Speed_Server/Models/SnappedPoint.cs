namespace Speed_Server.Models
{
    public class SnappedPoint
    {
        public LocationTime Location { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }

        public SnappedPoint(LocationTime location)
        {
            this.Location = location;
        }
    }
}
