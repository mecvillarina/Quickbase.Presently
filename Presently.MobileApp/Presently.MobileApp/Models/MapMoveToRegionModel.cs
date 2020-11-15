namespace Presently.MobileApp.Models
{
    public struct MapMoveToRegionModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RadiusInMeters { get; set; }

        public MapMoveToRegionModel(double latitude, double longitude, double radiusInMeters)
        {
            Latitude = latitude;
            Longitude = longitude;
            RadiusInMeters = radiusInMeters;
        }
    }
}
