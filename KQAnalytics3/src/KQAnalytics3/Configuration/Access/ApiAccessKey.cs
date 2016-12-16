using Newtonsoft.Json;

namespace KQAnalytics3.Configuration.Access
{
    public enum ApiAccessScope
    {
        Read,
        Write,
        Admin
    }

    public class ApiAccessKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("scopes")]
        public ApiAccessScope[] AccessScopes { get; set; } = new[] { ApiAccessScope.Read };
    }
}