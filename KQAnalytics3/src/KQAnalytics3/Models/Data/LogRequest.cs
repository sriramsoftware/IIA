using LiteDB;
using Newtonsoft.Json;
using System;

namespace KQAnalytics3.Models.Data
{
    public class LogRequest
    {
        /// <summary>
        /// Stores an identifier that should be unique to the request
        /// </summary>
        [JsonProperty("id")]
        public Guid Identifier { get; set; }

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
        public DateTime TimeStamp { get; internal set; } = DateTime.Now;

        [JsonIgnore]
        [BsonId]
        public ObjectId DatabaseId { get; set; }
    }
}