using WeatherAPI.Models;

namespace WeatherAPI.Integrators
{
    public interface IOpenWeatherMapIntegrator
    {
        Task<CurrentWeather?> CallCurrentWeatherData(double lat, double lon);
    }
}
