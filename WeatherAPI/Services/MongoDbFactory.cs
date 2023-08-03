using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherAPI.Engine.Settings;

namespace WeatherAPI.Services
{
    public class MongoDbFactory : IMongoDbFactory
    {
        private readonly IMongoClient _client;

        public MongoDbFactory(IOptions<Mongosettings> options)
        {
            if (options?.Value == null) throw new ArgumentNullException(nameof(options));

            _client = new MongoClient(options!.Value!.ConnectionString);
        }

        public IMongoCollection<T> GetCollection<T>(string databaseName, string collectionName)
        {
            if (string.IsNullOrEmpty(databaseName)) throw new ArgumentNullException(nameof(databaseName));
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));

            return _client.GetDatabase(databaseName).GetCollection<T>(collectionName);
        }
    }
}
