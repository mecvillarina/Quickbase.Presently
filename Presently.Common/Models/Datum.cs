using Newtonsoft.Json;

namespace Presently.Common.Models
{
    public class Datum
    {
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
