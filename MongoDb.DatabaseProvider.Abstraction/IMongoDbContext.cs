using MongoDB.Driver;

namespace MongoDb.DatabaseProvider;
public interface IMongoDbContext
{
    IMongoDatabase Db { get; }
}