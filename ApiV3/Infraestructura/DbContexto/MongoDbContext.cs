namespace ApiV3.Infraestructura.DbContexto
{
    public class MongoDbContext : IMongoDbContext
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IMongoDbContext
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
