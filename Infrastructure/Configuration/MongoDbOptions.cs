namespace Infrastructure.Configuration
{
    public class MongoDbOptions
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public MongoDbOptions() { }
    }
}
