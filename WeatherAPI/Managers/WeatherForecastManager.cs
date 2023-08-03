using System.Net;
using WeatherAPI.Commands;
using WeatherAPI.Engine.Exceptions;
using WeatherAPI.Entities;
using WeatherAPI.Integrators;
using WeatherAPI.Services;

namespace WeatherAPI.Managers
{
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private readonly ILogger<WeatherForecastManager> _logger;
        private readonly IOpenWeatherMapIntegrator _integrator;
        private readonly IDataAccessService _dataAccess;

        public WeatherForecastManager(
            ILogger<WeatherForecastManager> logger,
            IOpenWeatherMapIntegrator integrator,
            IDataAccessService dataAccess)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _integrator = integrator ?? throw new ArgumentNullException(nameof(integrator));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Result> GetWeatherForecast(GetWeatherForecastCommand command)
        {
            var validationResult = new GetWeatherForecastCommandValidator().Validate(command);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ToString(" "));

            var currentWeatherForecast = await _integrator.CallCurrentWeatherData(command.Latitude, command.Longitude);

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