using Newtonsoft.Json;

namespace KQAnalytics3.Configuration
{
    public class KQServerCorsOptions
    {
        [JsonProperty("origins")]
        public string[] Origins { get; set; } = new string[0];
    }
}