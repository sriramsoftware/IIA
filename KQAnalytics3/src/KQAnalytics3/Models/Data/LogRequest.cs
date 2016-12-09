using LiteDB;
using System;

namespace KQAnalytics3.Models.Data
{
    public class LogRequest
    {
        /// <summary>
        /// Stores an identifier that should be unique to the request
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Stores the query URL to the KQ server
        /// </summary>
        public string KQApiNode { get; set; }

        /// <summary>
        /// Stores the client address
        /// </summary>
        public string OriginAddress { get; set; }

        [BsonId]
        public ObjectId DatabaseId { get; set; }
    }
}