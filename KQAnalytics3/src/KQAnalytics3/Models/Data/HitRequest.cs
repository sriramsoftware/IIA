using Newtonsoft.Json;

namespace KQAnalytics3.Models.Data
{
    public class HitRequest : LogRequest
    {
        /// <summary>
        /// Represents an identifier for a page. For web sites, this can be a URL. For mobile apps,
        /// this can be a tag.
        /// </summary>
        [JsonProperty("pageId")]
        public string PageIdentifier { get; set; }
    }
}