using IridiumIon.Analytics.Models.Requests;
using Newtonsoft.Json;
using System;

namespace IridiumIon.Analytics.Models.Data
{
    public class LogRequest : DatabaseObject
    {
        /// <summary>
        /// Stores an identifier that should be unique to the request
        /// </summary>
        [JsonProperty("id")]
        public Guid Identifier { get; set; }

        /// <summary>
        /// Stores the identifier for the corresponding session. Can be used to look up a session
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionIdentifier { get; set; }

        /// <summary>
        /// Stores the query URL to the KQ server
        /// </summary>
        [JsonProperty("apiNode")]
        public string KQApiNode { get; set; }

        /// <summary>
        /// Stores the client address
        /// </summary>
        [JsonProperty("originAddress")]
        public string OriginAddress { get; set; }

        /// <summary>
        /// Stores the date and time the request was created
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Stores the request type flags
        /// </summary>
        [JsonProperty("requestType")]
        public DataRequestType RequestType { get; set; }

        [JsonProperty("kind")]
        public virtual string Kind { get; } = "log";
    }
}