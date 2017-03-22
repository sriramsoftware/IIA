using IridiumIon.Analytics.Configuration.Access;
using Newtonsoft.Json;

namespace IridiumIon.Analytics.Configuration
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
        public NAAccessKey[] ApiKeys { get; set; } = new NAAccessKey[0];

        [JsonProperty("databaseCryptoPass")]
        public string DatabaseEncryptionPassword { get; set; }

        [JsonProperty("pluginPaths")]
        public string[] PluginPaths { get; set; } = new string[0];
    }
}