# MongoDb.DatabaseProvider

Use `MongoDb.DatabaseProvider.Core` with one of:
- `MongoDb.DatabaseProvider.StaticProvider` to use `MongoDbContext` class with all extension methods to manipulate with MongoDB.
- `MongoDb.DatabaseProvider.Generators` to use `MongoDbCollectionAttribute` marking your classes and use source-generated optimized extension methods for each collection.