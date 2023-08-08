using WeatherAPI.Models;

namespace WeatherAPI.Clients
{
    public interface IOpenWeatherMapClient
    {
        Task<CurrentWeather?> CallCurrentWeatherData(double lat, double lon);
    }
}
