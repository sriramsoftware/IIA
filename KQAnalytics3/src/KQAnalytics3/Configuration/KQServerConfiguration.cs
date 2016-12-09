using Newtonsoft.Json;

namespace KQAnalytics3.Configuration
{
    public class KQServerConfiguration
    {
        [JsonProperty("cors")]
        public KQServerCorsOptions CorsOptions { get; set; }
    }
}