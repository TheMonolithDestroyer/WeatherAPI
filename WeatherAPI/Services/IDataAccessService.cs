using WeatherAPI.Entities;

namespace WeatherAPI.Services
{
    public interface IDataAccessService
    {
        Task CreateAsync(WeatherApiCallHistory entity);
        Task<WeatherApiCallHistory> GetAsync(string id);
    }
}
