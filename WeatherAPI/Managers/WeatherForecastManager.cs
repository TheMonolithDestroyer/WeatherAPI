using System.Net;
using WeatherAPI.Commands;
using WeatherAPI.Entities;
using WeatherAPI.Exceptions;
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

            await _dataAccess.CreateAsync(new WeatherApiCallHistory
            {
                Key = "Request",
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(command)
            });

            var currentWeatherForecast = await _integrator.CallCurrentWeatherData(command.Latitude, command.Longitude);
            await _dataAccess.CreateAsync(new WeatherApiCallHistory
            {
                Key = "Response",
                Data = currentWeatherForecast
            });

            return Result.Succeed(currentWeatherForecast, HttpStatusCode.Created);
        }
    }
}