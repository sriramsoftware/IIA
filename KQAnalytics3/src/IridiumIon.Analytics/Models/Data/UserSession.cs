using Newtonsoft.Json;
using System;

namespace KQAnalytics3.Models.Data
{
    public class UserSession : DatabaseObject
    {
        [JsonProperty("id")]
        public string SessionId { get; set; }

        public UserSession() : this(Guid.NewGuid())
        {
        }

        public UserSession(Guid customId)
        {
            SessionId = customId.ToString("N");
        }

        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }

        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }
    }
}