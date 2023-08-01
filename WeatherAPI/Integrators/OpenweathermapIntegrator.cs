namespace WeatherAPI.Integrators
{
    public class OpenweathermapIntegrator : IOpenweathermapIntegrator
    {
        private ILogger<OpenweathermapIntegrator> _logger;
        public OpenweathermapIntegrator(ILogger<OpenweathermapIntegrator> logger)
        {
            _logger = logger;
        }

        public async Task Tets()
        {

        }
    }
}
