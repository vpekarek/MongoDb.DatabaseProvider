using MongoDB.Driver;

namespace MongoDb.DatabaseProvider;
public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _db;
    public MongoDbContext(string connectionString, string database)
    {
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase(database);
    }
    public MongoDbContext(string database)
    {
        var client = new MongoClient();
        _db = client.GetDatabase(database);
    }
    public MongoDbContext(MongoClientSettings settings, string database)
    {
        var client = new MongoClient(settings);
        _db = client.GetDatabase(database);
    }
    public MongoDbContext(MongoUrl url, string database)
    {
        var client = new MongoClient(url);
        _db = client.GetDatabase(database);
    }

    public IMongoDatabase Db => _db;
}