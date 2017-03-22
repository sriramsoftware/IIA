using Newtonsoft.Json;

namespace IridiumIon.Analytics.Configuration.Access
{
    /// <summary>
    /// An enumeration of access scope identifiers, used for granular permission grants on key access
    /// </summary>
    public enum ApiAccessScope
    {
        None = 1 << 0,
        Read = 1 << 1,
        Query = Read,
        Admin = 1 << 31,
    }

    public class NAAccessKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("scopes")]
        public ApiAccessScope[] AccessScopes { get; set; } = new[] { ApiAccessScope.Read };
    }
}