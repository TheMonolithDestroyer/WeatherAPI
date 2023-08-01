using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Commands;
using WeatherAPI.Managers;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastManager _manager;
        public WeatherForecastController(IWeatherForecastManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(_manager));
        }

        public async Task<IActionResult> GetWeatherForecast(GetWeatherForecastCommand command)
        {
            return Ok();
        }
    }
}
