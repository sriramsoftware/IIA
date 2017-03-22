using Newtonsoft.Json;

namespace IridiumIon.Analytics.Configuration
{
    public class NADatabaseConfiguration
    {
        [JsonProperty("fileName")]
        public string FileName { get; internal set; } = "ii-analytics.lidb";
    }
}