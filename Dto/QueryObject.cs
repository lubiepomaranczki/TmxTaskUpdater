using Newtonsoft.Json;

namespace TmxTaskUpdater.Dto
{
    public class QueryRequest
    {
        public QueryRequest(string query)
        {
            Query = query;
        }

        [JsonProperty("query")]
        public string Query { get; set; }
    }
}
