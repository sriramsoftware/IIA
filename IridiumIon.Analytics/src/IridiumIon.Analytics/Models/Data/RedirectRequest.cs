using Newtonsoft.Json;

namespace IridiumIon.Analytics.Models.Data
{
    public class RedirectRequest : LogRequest
    {
        [JsonProperty("destUrl")]
        public string DestinationUrl { get; set; }

        public override string Kind { get; } = "redirect";
    }
}