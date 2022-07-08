using Microsoft.Extensions.DependencyInjection;
using MongoDb.DatabaseProvider;

IServiceCollection? collection = null;
collection?.AddMongoDbProvider("data");