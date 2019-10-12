using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace portal.webapi.Models
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        Location location { get; set; }
        public string device_name { get; set; }
    }

    public class WorkoutDevice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
    }
}