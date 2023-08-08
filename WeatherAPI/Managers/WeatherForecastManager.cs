using System.Net;
using WeatherAPI.Clients;
using WeatherAPI.Commands;
using WeatherAPI.Engine.Exceptions;
using WeatherAPI.Entities;
using WeatherAPI.Services;

namespace WeatherAPI.Managers
{
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private readonly ILogger<WeatherForecastManager> _logger;
        private readonly IOpenWeatherMapClient _openWeatherMapClient;
        private readonly IDataAccessService _dataAccess;

        public WeatherForecastManager(
            ILogger<WeatherForecastManager> logger,
            IOpenWeatherMapClient openWeatherMapClient,
            IDataAccessService dataAccess)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _openWeatherMapClient = openWeatherMapClient ?? throw new ArgumentNullException(nameof(openWeatherMapClient));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Result> GetWeatherForecast(GetWeatherForecastCommand command)
        {
            var validationResult = new GetWeatherForecastCommandValidator().Validate(command);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ToString(" "));

            var currentWeatherForecast = await _openWeatherMapClient.CallCurrentWeatherData(command.Latitude, command.Longitude);

            var data = new WeatherApiCallHistory()
            {
                RequestData = command,
                ResponseData = currentWeatherForecast
            };
            await _dataAccess.CreateAsync(data);

            _logger.LogInformation($"Created a document 'WeatherApiCallHistory' with _id: { data.Id }");

            return Result.Succeed(currentWeatherForecast, HttpStatusCode.Created);
        }
    }
}