using WeatherAPI.Commands;

namespace WeatherAPI.Managers
{
    public interface IWeatherForecastManager
    {
        Task<Result> GetWeatherForecast(GetWeatherForecastCommand command);
    }
}
