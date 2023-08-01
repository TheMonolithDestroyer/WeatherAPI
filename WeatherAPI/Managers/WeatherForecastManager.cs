using WeatherAPI.Services;

namespace WeatherAPI.Managers
{
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private readonly ILogger<WeatherForecastManager> _logger;
        private readonly IMongoDBService _mongoService;

        public WeatherForecastManager(
            ILogger<WeatherForecastManager> logger,
            IMongoDBService mongoService)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _mongoService = mongoService ?? throw new ArgumentNullException(nameof(_mongoService));
        }
    }
}