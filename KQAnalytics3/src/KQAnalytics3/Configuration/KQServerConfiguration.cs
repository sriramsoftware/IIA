using Newtonsoft.Json;

namespace KQAnalytics3.Configuration
{
    public class KQServerConfiguration
    {
        [JsonProperty("cors")]
        public KQServerCorsOptions CorsOptions { get; set; }

        [JsonProperty("blacklist")]
        public BlacklistConfiguration BlacklistConfiguration { get; set; }

        [JsonProperty("whitelist")]
        public WhitelistConfiguration WhitelistConfiguration { get; set; }
    }
}