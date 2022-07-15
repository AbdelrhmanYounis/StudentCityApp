using MongoDB.Driver;

namespace StudentCity.DAL.Shared
{
    public class DbSettings
    {
        public IMongoDatabase Database;
        public string ServerAddress { get; set; } = "mongodb://localhost:27017";
        public string DataBaseName { get; set; } = "StudentCity";
    }
}
