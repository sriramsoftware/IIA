using Newtonsoft.Json;

namespace KQAnalytics3.Configuration
{
    public class BlacklistConfiguration
    {
        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("hosts")]
        public string[] Hosts { get; set; } = new string[0];
    }
}