using Newtonsoft.Json;

namespace IridiumIon.Analytics.Configuration
{
    public class KQServerCorsOptions
    {
        [JsonProperty("origins")]
        public string[] Origins { get; set; } = new string[0];
    }
}