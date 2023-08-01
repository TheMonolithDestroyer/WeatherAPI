using MongoDB.Driver;

namespace WeatherAPI.Services
{
    public interface IMongoDbFactory
    {
        IMongoCollection<T> GetCollection<T>(string databaseName, string collectionNme);
    }
}
