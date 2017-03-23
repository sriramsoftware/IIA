using IridiumIon.Analytics.Configuration.Access;
using Newtonsoft.Json;

namespace IridiumIon.Analytics.Configuration
{
    public class NAServerParameters
    {
        [JsonProperty("databaseConfiguration")]
        public NADatabaseConfiguration DatabaseConfiguration { get; set; }

        /// <summary>
        /// Master API keys. These will also be stored in the state but will not be duplicated.
        /// </summary>
        [JsonProperty("apikeys")]
        public NAAccessKey[] ApiKeys { get; set; } = new NAAccessKey[0];

        /// <summary>
        /// If set to true, the keys will be reset upon a server start.
        /// </summary>
        public bool KeyReset { get; set; } = false;

        [JsonProperty("corsOrigins")]
        public string[] CorsOrigins { get; set; } = new string[0];

        public string BaseDirectory { get; internal set; }
    }
}