using KQAnalytics3.Configuration.Access;
using Newtonsoft.Json;

namespace KQAnalytics3.Configuration
{
    public class KQServerConfiguration
    {
        [JsonProperty("cors")]
        public KQServerCorsOptions CorsOptions { get; set; }

        [JsonProperty("blacklist")]
        public FilterListConfiguration BlacklistConfiguration { get; set; }

        [JsonProperty("whitelist")]
        public FilterListConfiguration WhitelistConfiguration { get; set; }

        [JsonProperty("apiKeys")]
        public ApiAccessKey[] ApiKeys { get; set; } = new ApiAccessKey[0];

        [JsonProperty("databaseCryptoPass")]
        public string DatabaseEncryptionPassword { get; set; }
    }
}