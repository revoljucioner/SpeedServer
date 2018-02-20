namespace Speed_Server.Models
{
    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double elevation { get; set; }

        public override string ToString()
        {
            string str = latitude + "," + longitude;
            return str;
        }
    }
}
