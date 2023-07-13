namespace RealStateMVCWebApp.Models.Entities
{
    public class Address
    {
        public string DetailedAddress { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Location Location { get; set; } = new Location();
    }
}
