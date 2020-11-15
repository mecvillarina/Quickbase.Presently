using Newtonsoft.Json;
using System.Collections.Generic;

namespace Presently.Common.Models
{
    public class PostResponseModel
    {
        [JsonProperty("data")]
        public List<Dictionary<string, Datum>> Data { get; set; } = new List<Dictionary<string, Datum>>();
    }
}
