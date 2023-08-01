using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherAPI.Entities
{
    public class WeatherApiCallHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Key { get; set; }
        public object? Data { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
    }
}
