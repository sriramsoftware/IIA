using LiteDB;
using Newtonsoft.Json;

namespace IridiumIon.Analytics.Models.Data
{
    public class DatabaseObject
    {
        [JsonIgnore]
        [BsonId]
        public ObjectId DatabaseId { get; set; }
    }
}