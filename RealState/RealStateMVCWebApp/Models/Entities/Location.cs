namespace RealStateMVCWebApp.Models.Entities
{
    public class Location
    {
        public string Type { get; set; } = string.Empty;
        public double LanCoordinate { get; set; } 
        public double LonCoordinate { get; set; }
        public bool IsLocationExact { get; set; } = false;
    }
}
