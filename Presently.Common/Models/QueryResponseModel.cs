using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presently.Common.Models
{
    public class QueryResponseModel
    {
        [JsonProperty("data")]
        public List<Dictionary<string, Datum>> Data { get; set; } = new List<Dictionary<string, Datum>>();

        [JsonProperty("fields")]
        public List<QueryResponseField> Fields { get; set; } = new List<QueryResponseField>();

        [JsonProperty("metadata")]
        public QueryResponseMetadata Metadata { get; set; } = new QueryResponseMetadata();
    }

    public class QueryResponseField
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class QueryResponseMetadata
    {
        [JsonProperty("numFields")]
        public long NumFields { get; set; }

        [JsonProperty("numRecords")]
        public long NumRecords { get; set; }

        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("totalRecords")]
        public long TotalRecords { get; set; }
    }
}
