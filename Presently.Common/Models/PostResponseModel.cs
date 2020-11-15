using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presently.Common.Models
{
    public class PostResponseModel
    {
        [JsonProperty("data")]
        public List<Dictionary<string, Datum>> Data { get; set; } = new List<Dictionary<string, Datum>>();
    }
}
