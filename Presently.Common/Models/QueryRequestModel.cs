using Newtonsoft.Json;
using System.Collections.Generic;

namespace Presently.Common.Models
{
    public class QueryRequestModel
    {
        [JsonProperty("from")]
        public string From { get; private set; }

        [JsonProperty("select")]
        public List<long> Select { get; set; } = new List<long>();

        [JsonProperty("where")]
        public string Where { get; set; } = string.Empty;

        [JsonProperty("sortBy")]
        public List<QueryRequestSortBy> SortBy { get; set; } = new List<QueryRequestSortBy>();

        [JsonProperty("groupBy")]
        public List<QueryRequestGroupBy> GroupBy { get; set; } = new List<QueryRequestGroupBy>();

        [JsonProperty("options")]
        public QueryRequestOptions Options { get; set; } = new QueryRequestOptions();

        public QueryRequestModel(string dbId)
        {
            From = dbId;
        }
    }

    public class QueryRequestGroupBy
    {
        [JsonProperty("fieldId")]
        public long FieldId { get; set; }

        [JsonProperty("grouping")]
        public string Grouping { get; set; }
    }

    public class QueryRequestSortBy
    {
        [JsonProperty("fieldId")]
        public long FieldId { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
    }

    public class QueryRequestOptions
    {
        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("top")]
        public long Top { get; set; }

        [JsonProperty("compareWithAppLocalTime")]
        public bool CompareWithAppLocalTime { get; set; }
    }
}
