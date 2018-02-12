namespace SpeedServerApi.Models
{
    public class SpeedModel
    {
        //public long Id { get; set; }
        //public string Name { get; set; }
        //public bool IsComplete { get; set; }
        //public int latitude { get; set; }
        public SnappedPoint[] snappedPoints { get; set; }
    }
    public class SnappedPoint
    {
        public Location Location { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}