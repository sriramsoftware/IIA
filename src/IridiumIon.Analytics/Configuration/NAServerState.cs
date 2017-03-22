using IridiumIon.Analytics.Configuration.Access;
using LiteDB;
using System;
using System.Collections.Generic;

namespace IridiumIon.Analytics.Configuration
{
    /// <summary>
    /// Persisted state for the server
    /// </summary>
    public class NAServerState
    {
        public List<NAAccessKey> ApiKeys { get; set; } = new List<NAAccessKey>();

        /// <summary>
        /// BsonDocument ID
        /// </summary>
        [BsonId]
        public ObjectId DatabaseId { get; set; }

        [BsonIgnore]
        public LiteCollection<NAServerState> PersistenceMedium { get; set; }

        [BsonIgnore]
        public Action Persist { get; set; }
    }
}