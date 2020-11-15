using Newtonsoft.Json;
using System.Collections.Generic;

namespace Presently.Common.Models
{
    public class PostRequestModel
    {
        [JsonProperty("to")]
        public string To { get; private set; }

        [JsonProperty("data")]
        public List<Dictionary<string, Datum>> Data { get; set; } = new List<Dictionary<string, Datum>>();

        [JsonProperty("fieldsToReturn")]
        public List<long> FieldsToReturn { get; set; } = new List<long>();

        public PostRequestModel(string to)
        {
            To = to;
        }
    }
}
