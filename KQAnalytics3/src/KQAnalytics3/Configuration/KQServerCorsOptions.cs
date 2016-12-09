using Newtonsoft.Json;

namespace KQAnalytics3.Configuration
{
    public class KQServerCorsOptions
    {
        [JsonProperty("origins")]
        public string[] Origins = new string[0];
    }
}