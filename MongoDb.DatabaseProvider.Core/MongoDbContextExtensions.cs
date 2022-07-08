using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb.DatabaseProvider;

public static partial class MongoDbContextExtensions
{
    public static void InsertRecord<T>(this IMongoDbContext context, string table, T record, InsertOneOptions? insertOneOptions = null)
    {
        var collection = context.Db.GetCollection<T>(table);
        insertOneOptions ??= new InsertOneOptions { };
        collection.InsertOne(record, insertOneOptions);
    }

    public static Task InsertRecordAsync<T>(this IMongoDbContext context, string table, T record, InsertOneOptions? insertOneOptions = null, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        insertOneOptions ??= new InsertOneOptions { };
        return collection.InsertOneAsync(record, insertOneOptions, cancellationToken);
    }

    public static void InsertRecords<T>(this IMongoDbContext context, string table, IEnumerable<T> records, InsertManyOptions? insertManyOptions = null)
    {
        var collection = context.Db.GetCollection<T>(table);
        insertManyOptions ??= new InsertManyOptions { IsOrdered = false };
        collection.InsertMany(records, insertManyOptions);
    }
    public static Task InsertRecordsAsync<T>(this IMongoDbContext context, string table, IEnumerable<T> records, InsertManyOptions? insertManyOptions = null, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        insertManyOptions ??= new InsertManyOptions { IsOrdered = false };
        return collection.InsertManyAsync(records, insertManyOptions, cancellationToken);
    }

    public static List<T> LoadRecords<T>(this IMongoDbContext context, string table)
    {
        var collection = context.Db.GetCollection<T>(table);
        return collection.Find(new BsonDocument()).ToList();
    }
    public static Task<List<T>> LoadRecordsAsync<T>(this IMongoDbContext context, string table, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        return collection.Find(new BsonDocument()).ToListAsync(cancellationToken);
    }

    public static T LoadRecordById<T>(this IMongoDbContext context, string table, Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return context.LoadRecordBy<T>(table, filter);
    }
    public static Task<T> LoadRecordByIdAsync<T>(this IMongoDbContext context, string table, Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return context.LoadRecordByAsync<T>(table, filter);
    }

    public static IEnumerable<T> LoadRecordsById<T>(this IMongoDbContext context, string table, params Guid[] ids)
    {
        var filter = Builders<T>.Filter.AnyIn("Id", ids);
        return context.LoadRecordsBy<T>(table, filter);
    }
    public static IEnumerable<T> LoadRecordsById<T>(this IMongoDbContext context, string table, IEnumerable<Guid[]> ids)
    {
        var filter = Builders<T>.Filter.AnyIn("Id", ids);
        return context.LoadRecordsBy<T>(table, filter);
    }
    public static Task<IEnumerable<T>> LoadRecordsByIdAsync<T>(this IMongoDbContext context, string table, params Guid[] ids)
    {
        var filter = Builders<T>.Filter.AnyIn("Id", ids);
        return context.LoadRecordsByAsync<T>(table, filter);
    }
    public static Task<IEnumerable<T>> LoadRecordsByIdAsync<T>(this IMongoDbContext context, string table, IEnumerable<Guid> ids)
    {
        var filter = Builders<T>.Filter.AnyIn("Id", ids);
        return context.LoadRecordsByAsync<T>(table, filter);
    }

    public static T LoadRecordBy<T>(this IMongoDbContext context, string table, FilterDefinition<T> filter)
    {
        var collection = context.Db.GetCollection<T>(table);
        return collection.Find(filter).First();
    }
    public static async Task<T> LoadRecordByAsync<T>(this IMongoDbContext context, string table, FilterDefinition<T> filter, FindOptions<T, T>? options = null, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        var result = await collection.FindAsync(filter, options, cancellationToken);
        return await result.FirstAsync(cancellationToken);
    }

    public static IEnumerable<T> LoadRecordsBy<T>(this IMongoDbContext context, string table, FilterDefinition<T> filter)
    {
        var collection = context.Db.GetCollection<T>(table);
        return collection.Find(filter).ToList();
    }
    public static async Task<IEnumerable<T>> LoadRecordsByAsync<T>(this IMongoDbContext context, string table, FilterDefinition<T> filter, FindOptions<T, T>? options = null, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        var result = await collection.FindAsync(filter, options, cancellationToken);
        return await result.ToListAsync(cancellationToken);
    }

    public static async Task<IEnumerable<T>> LoadRecordsByAsync<T, TEntity>(this IMongoDbContext context, string table, (System.Linq.Expressions.Expression<Func<T, TEntity>> PropertyName, TEntity PropertyValue) filterValue, FindOptions<T, T>? options = null, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq(filterValue.PropertyName, filterValue.PropertyValue);
        var result = await collection.FindAsync(filter, options, cancellationToken);
        return await result.ToListAsync(cancellationToken);
    }

    public static ReplaceOneResult? UpsertRecord<T>(this IMongoDbContext context, string table, Guid id, T record, ReplaceOptions? replaceOptions = null)
    {
        var binData = new BsonBinaryData(id, GuidRepresentation.Standard);
        var filter = new BsonDocument("_id", binData);
        return context.UpsertRecord(table, filter, record, replaceOptions);
    }
    public static ReplaceOneResult? UpsertRecord<T>(this IMongoDbContext context, string table, FilterDefinition<T> filter, T record, ReplaceOptions? replaceOptions = null)
    {
        replaceOptions ??= new ReplaceOptions { IsUpsert = true };
        var collection = context.Db.GetCollection<T>(table);
        var result = collection.ReplaceOne(filter, record, replaceOptions);
        return result;
    }

    public static Task<ReplaceOneResult?> UpsertRecordAsync<T>(this IMongoDbContext context, string table, Guid id, T record, ReplaceOptions? replaceOptions = null, CancellationToken cancellationToken = default)
    {
        var binData = new BsonBinaryData(id, GuidRepresentation.Standard);
        var filter = new BsonDocument("_id", binData);
        return context.UpsertRecordAsync<T>(table, filter, record, replaceOptions, cancellationToken);
    }
    public static Task<ReplaceOneResult?> UpsertRecordAsync<T>(this IMongoDbContext context, string table, FilterDefinition<T> filter, T record, ReplaceOptions? replaceOptions = null, CancellationToken cancellationToken = default)
    {
        replaceOptions ??= new ReplaceOptions { IsUpsert = true };
        var collection = context.Db.GetCollection<T>(table);
        var result = collection.ReplaceOneAsync(filter, record, replaceOptions, cancellationToken);
        return result;
    }

    public static DeleteResult? DeleteRecord<T>(this IMongoDbContext context, string table, Guid id)
    {
        var collection = context.Db.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("Id", id);
        var result = collection.DeleteOne(filter);
        return result;
    }
    public static Task<DeleteResult?> DeleteRecordAsync<T>(this IMongoDbContext context, string table, Guid id, CancellationToken cancellationToken = default)
    {
        var collection = context.Db.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("Id", id);
        return collection.DeleteOneAsync(filter, cancellationToken);
    }
}
