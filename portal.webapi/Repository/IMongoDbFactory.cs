using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using portal.webapi.Models;

namespace portal.webapi.Repository
{
    public interface IMongoDatabaseFactory
    {
        IMongoDatabase Connect(IConfiguration config, WorkoutsDatabaseSettings settings);
    }

    public interface IRepositoryFactory
    {
        IMongoDatabaseFactory MongoDbFactory { get; set; }
        IRepository<Workout> Create<T>(IConfiguration config, WorkoutsDatabaseSettings settings);
    }
}