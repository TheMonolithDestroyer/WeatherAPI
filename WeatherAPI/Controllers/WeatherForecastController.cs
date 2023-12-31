﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Commands;
using WeatherAPI.Managers;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastManager _manager;
        public WeatherForecastController(IWeatherForecastManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(_manager));
        }

        [HttpPost]
        public async Task<IActionResult> GetWeatherForecast([FromBody]GetWeatherForecastCommand command)
        {
            return StatusCode(201, await _manager.GetWeatherForecast(command));
        }
    }
}
