using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace portal.webapi.Models
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
    public class WorkoutLocation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
}