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

        [JsonProperty("scope")]
        public ApiAccessScope AccessScope { get; set; } = ApiAccessScope.Read;
    }
}