using MongoDB.Driver;

namespace MongoDb.DatabaseProvider;

public class MongoDbContextCollection<T>
{
    public IMongoCollection<T> Collection { get; set; }

    public MongoDbContextCollection(IMongoCollection<T> collection)
    {
        Collection = collection;
    }
}
