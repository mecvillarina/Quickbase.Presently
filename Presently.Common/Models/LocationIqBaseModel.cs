using Newtonsoft.Json;
using System;

namespace Presently.Common.Models
{
    public class LocationIqBaseModel
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("licence")]
        public Uri Licence { get; set; }

        [JsonProperty("osm_type")]
        public string OsmType { get; set; }

        [JsonProperty("osm_id")]
        public string OsmId { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lon")]
        public string Lon { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("address")]
        public LocationIqAddressModel Address { get; set; }
    }

    public class LocationIqAddressModel
    {
        [JsonProperty("address29")]
        public string Address29 { get; set; }

        [JsonProperty("road")]
        public string Road { get; set; }

        [JsonProperty("village")]
        public string Village { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}
