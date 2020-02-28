using Newtonsoft.Json;

namespace TmxTaskUpdater.Dto
{
    public class UpdateItemRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
