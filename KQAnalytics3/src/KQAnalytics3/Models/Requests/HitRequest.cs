namespace KQAnalytics3.Models.Requests
{
    public class HitRequest : LogRequest
    {
        /// <summary>
        /// Represents an identifier for a page. For web sites, this can be a URL. For mobile apps,
        /// this can be a tag.
        /// </summary>
        public string PageIdentifier { get; set; }
    }
}