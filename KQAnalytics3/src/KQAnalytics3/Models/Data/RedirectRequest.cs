using Newtonsoft.Json;

namespace KQAnalytics3.Models.Data
{
    public class RedirectRequest : LogRequest
    {
        [JsonProperty("destUrl")]
        public string DestinationUrl { get; set; }

        [JsonProperty("kind")]
        public override string Kind { get; } = "redirect";
    }
}