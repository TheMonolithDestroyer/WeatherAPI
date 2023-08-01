namespace WeatherAPI.Integrators
{
    public interface IOpenWeatherMapIntegrator
    {
        Task<string> CallCurrentWeatherData(double lat, double lon);
    }
}
