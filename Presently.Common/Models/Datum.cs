using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presently.Common.Models
{
    public class Datum
    {
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
