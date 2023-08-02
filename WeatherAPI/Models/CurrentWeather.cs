using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace WeatherAPI.Models
{
    public class CurrentWeather
    {
        [JsonProperty("coord")]
        public Coord? Coord { get; set; }
        [JsonProperty("weather")]
        [BsonIgnoreIfNull]
        public List<Weather>? Weather { get; set; }
        [JsonProperty("base")]
        [BsonIgnoreIfNull]
        public string? Base { get; set; }
        [JsonProperty("main")]
        [BsonIgnoreIfNull]
        public Foremost? Main { get; set; }
        [JsonProperty("visibility")]
        [BsonIgnoreIfNull]
        public double? Visibility { get; set; }
        [JsonProperty("wind")]
        [BsonIgnoreIfNull]
        public Wind? Wind { get; set; }
        [JsonProperty("clouds")]
        [BsonIgnoreIfNull]
        public Clouds? Clouds { get; set; }
        [JsonProperty("dt")]
        [BsonIgnoreIfNull]
        public long? Dt { get; set; }
        [JsonProperty("sys")]
        [BsonIgnoreIfNull]
        public Sys? Sys { get; set; }
        [JsonProperty("timezone")]
        [BsonIgnoreIfNull]
        public long? Timezone { get; set; }
        [JsonProperty("id")]
        [BsonIgnoreIfNull]
        public long? Id { get; set; }
        [JsonProperty("name")]
        [BsonIgnoreIfNull]
        public string? Name { get; set; }
        [JsonProperty("cod")]
        [BsonIgnoreIfNull]
        public int? Cod { get; set; }
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        [BsonIgnoreIfNull]
        public int? Id { get; set; }
        [JsonProperty("main")]
        [BsonIgnoreIfNull]
        public string? Main { get; set; }
        [JsonProperty("description")]
        [BsonIgnoreIfNull]
        public string? Description { get; set; }
        [JsonProperty("icon")]
        [BsonIgnoreIfNull]
        public string? Icon { get; set; }
    }

    public class Foremost
    {
        [JsonProperty("temp")]
        [BsonIgnoreIfNull]
        public double? Temp { get; set; }
        [JsonProperty("feels_like")]
        [BsonIgnoreIfNull]
        public double? FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        [BsonIgnoreIfNull]
        public double? TempMin { get; set; }
        [JsonProperty("temp_max")]
        [BsonIgnoreIfNull]
        public double? TempMax { get; set; }
        [JsonProperty("pressure")]
        [BsonIgnoreIfNull]
        public double? Pressure { get; set; }
        [JsonProperty("humidity")]
        [BsonIgnoreIfNull]
        public int? Humidity { get; set; }
        [JsonProperty("sea_level")]
        [BsonIgnoreIfNull]
        public int? SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        [BsonIgnoreIfNull]
        public int? GrndLevel { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        [BsonIgnoreIfNull]
        public double? Speed { get; set; }
        [JsonProperty("deg")]
        [BsonIgnoreIfNull]
        public double? Deg { get; set; }
        [JsonProperty("gust")]
        [BsonIgnoreIfNull]
        public double? Gust { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        [BsonIgnoreIfNull]
        public long? All { get; set; }
    }

    public class Sys
    {
        [JsonProperty("type")]
        [BsonIgnoreIfNull]
        public int? Type { get; set; }
        [JsonProperty("id")]
        [BsonIgnoreIfNull]
        public long? Id { get; set; }
        [JsonProperty("country")]
        [BsonIgnoreIfNull]
        public string? Country { get; set; }
        [JsonProperty("sunrise")]
        [BsonIgnoreIfNull]
        public long? Sunrise { get; set; }
        [JsonProperty("sunset")]
        [BsonIgnoreIfNull]
        public long? Sunset { get; set; }
    }
}
