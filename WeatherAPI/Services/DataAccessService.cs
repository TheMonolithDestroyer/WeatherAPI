using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherAPI.Engine.Settings;
using WeatherAPI.Entities;

namespace WeatherAPI.Services
{
    public class DataAccessService : IDataAccessService
    {
        private readonly Mongosettings _settings;
        private readonly IMongoCollection<WeatherApiCallHistory> _collection;

        public DataAccessService(IOptions<Mongosettings> options, IMongoDbFactory dbFactory)
        {
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options.Value));
            _collection = dbFactory.GetCollection<WeatherApiCallHistory>(_settings!.Database!, _settings!.WeatherApiCallHistoryCollection!);
        }

        public async Task CreateAsync(WeatherApiCallHistory entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<WeatherApiCallHistory> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var results = await _collection.FindAsync(x => x.Id == id);
            return await results.SingleAsync();
        }
    }
}
