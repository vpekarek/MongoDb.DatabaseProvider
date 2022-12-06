namespace MongoDb.DatabaseProvider;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MongoDbCollectionAttribute : Attribute
{
    public string Type { get; set; } = "Guid";
}
