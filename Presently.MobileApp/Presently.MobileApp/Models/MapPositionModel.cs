namespace Presently.MobileApp.Models
{
    public struct MapPositionModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public MapPositionModel(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
