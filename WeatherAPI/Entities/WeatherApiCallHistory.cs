using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WeatherAPI.Commands;
using WeatherAPI.Models;

namespace WeatherAPI.Entities
{
    public class WeatherApiCallHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public GetWeatherForecastCommand? RequestData { get; set; }
        public CurrentWeather? ResponseData { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
    }
}
