using LiteDB;
using Newtonsoft.Json;

namespace KQAnalytics3.Models.Data
{
    public class DatabaseObject
    {
        [JsonIgnore]
        [BsonId]
        public ObjectId DatabaseId { get; set; }
    }
}