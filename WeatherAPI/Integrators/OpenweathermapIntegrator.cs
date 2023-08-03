using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WeatherAPI.Engine.Settings;
using WeatherAPI.Models;

namespace WeatherAPI.Integrators
{
    public class OpenWeatherMapIntegrator : IOpenWeatherMapIntegrator
    {
        private readonly ILogger<OpenWeatherMapIntegrator> _logger;
        private readonly OpenweathermapApisettings _settings;
        private readonly HttpClient _httpClient;

        public OpenWeatherMapIntegrator(
            ILogger<OpenWeatherMapIntegrator> logger,
            IOptions<OpenweathermapApisettings> options,
            HttpClient httpClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            httpClient.Timeout = TimeSpan.FromMinutes(5);
            httpClient.MaxResponseContentBufferSize = int.MaxValue;
            httpClient.BaseAddress = new Uri(_settings!.BaseUri!);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CurrentWeather?> CallCurrentWeatherData(double lat, double lon)
        {
            var currentWeatherUri = string.Format(_settings!.CurrentWeatherUri!, lat, lon, _settings.ApiKey);

            var request = new HttpRequestMessage(HttpMethod.Get, currentWeatherUri);
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Unsuccessful API call.", null, response.StatusCode);

            var result = JsonConvert.DeserializeObject<CurrentWeather>(responseContent);

            _logger.LogInformation($"Successful API call: {JsonConvert.SerializeObject(result)}");

            return result;
        }
    }
}
