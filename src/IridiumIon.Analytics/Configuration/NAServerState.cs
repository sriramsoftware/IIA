using IridiumIon.Analytics.Configuration.Access;
using LiteDB;
using OsmiumSubstrate.Configuration;
using OsmiumSubstrate.Configuration.Access;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IridiumIon.Analytics.Configuration
{
    /// <summary>
    /// Persisted state for the server
    /// </summary>
    public class NAServerState : ISubstrateServerState<NAAccessKey, NAApiAccessScope>
    {
        public List<NAAccessKey> ApiKeys { get; set; }

        IEnumerable<NAAccessKey> ISubstrateServerState<NAAccessKey, NAApiAccessScope>.ApiKeys => ApiKeys;

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