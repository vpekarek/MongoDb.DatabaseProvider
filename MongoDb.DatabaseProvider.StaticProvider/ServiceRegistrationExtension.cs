using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoDb.DatabaseProvider;
public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddMongoDbProvider(this IServiceCollection services, string connectionString, string database)
    {
        services.AddTransient<IMongoDbContext>((serviceCollection) =>
        {
            var dbContext = new MongoDbContext(connectionString, database);
            return dbContext;
        });

        return services;
    }

    public static IServiceCollection AddMongoDbProvider(this IServiceCollection services, string database)
    {
        services.AddTransient<IMongoDbContext>((serviceCollection) =>
        {
            var dbContext = new MongoDbContext(database);
            return dbContext;
        });

        return services;
    }

    public static IServiceCollection AddMongoDbProvider(this IServiceCollection services, MongoClientSettings settings, string database)
    {
        services.AddTransient<IMongoDbContext>((serviceCollection) =>
        {
            var dbContext = new MongoDbContext(settings, database);
            return dbContext;
        });

        return services;
    }

    public static IServiceCollection AddMongoDbProvider(this IServiceCollection services, MongoUrl url, string database)
    {
        services.AddTransient<IMongoDbContext>((serviceCollection) =>
        {
            var dbContext = new MongoDbContext(url, database);
            return dbContext;
        });

        return services;
    }
}
