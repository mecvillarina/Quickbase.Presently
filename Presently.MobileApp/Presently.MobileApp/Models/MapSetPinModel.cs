using System;
using System.Collections.Generic;
using System.Text;

namespace Presently.MobileApp.Models
{
    public class MapSetPinModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PinSource { get; set; }
        public object Tag { get; set; }
        public bool IsDraggable { get; set; }

        public MapSetPinModel(double latitude, double longitude, string pinSource, object tag)
        {
            Latitude = latitude;
            Longitude = longitude;
            PinSource = pinSource;
            Tag = tag;
        }
    }
}
