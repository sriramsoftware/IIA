using Newtonsoft.Json;

namespace KQAnalytics3.Models.Data
{
    public class TagRequest : LogRequest
    {
        /// <summary>
        /// A short custom tag field
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// A custom data field that holds a string
        /// </summary>
        [JsonProperty("data")]
        public string ExtraData { get; set; }

        public override string Kind { get; } = "tag";
    }
}