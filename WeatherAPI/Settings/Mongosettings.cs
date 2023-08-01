namespace WeatherAPI.Settings
{
    public class Mongosettings
    {
        public string? ConnectionString { get; set; }
        public string? Database { get; set; }
        public string? WeatherApiCallHistoryCollection { get; set; }
    }
}
