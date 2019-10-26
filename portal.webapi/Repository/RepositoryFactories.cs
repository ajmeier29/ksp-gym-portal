using System;
using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

using portal.webapi.Models;
using portal.webapi.Services;

namespace portal.webapi.Repository
{

    public class ProductionRepository : IMongoDatabaseFactory
    {
        public IMongoDatabase Connect(IConfiguration config, WorkoutsDatabaseSettings settings)
        {
            if(config == null)
            {
                throw new InvalidOperationException("No IConfiguration found in ProductionRepository");
            }
            if(settings == null)
            {
                throw new InvalidOperationException("No WorkoutsDatabaseSettings found in ProductionRepository");
            }
            string settingsObjectName = settings.GetType().ToString().Split('.').Last();
            // Then bind it to the IWorkoutDatabaseSettings object
            config.GetSection(settingsObjectName).Bind(settings);
            // Init connection string and connect to db
            settings.ConnectionString = ModifyMongoConnectionString(settings, config);
            MongoClient _client = new MongoClient(settings.ConnectionString);
            return _client.GetDatabase(settings.DatabaseName);
        }
        // Replaces the connection string with the values passed in from the env vars or user secrets.
        // Example connection string: mongodb+srv://[[MONGODBUSER]]:[[MONGODBPASSWORD]]@[[MONGODBHOSTNAME]]
        public string ModifyMongoConnectionString(WorkoutsDatabaseSettings settings, IConfiguration config)
        {
            ConfigurationService service = new ConfigurationService(settings, config);
            return service.ConnectionStringBuilder();
        }
    }
    public class RepositoryFactory : IRepositoryFactory
    {
        public IMongoDatabaseFactory MongoDbFactory { get; set; }
        public RepositoryFactory(IMongoDatabaseFactory dbFactory)
        {
            if (dbFactory == null)
            {
                throw new ArgumentException("dbFactory was null in RepositoryFactory!!");
            }
            MongoDbFactory = dbFactory;
        }

        public IRepository<Workout> Create<T>(IConfiguration config, WorkoutsDatabaseSettings settings)
        {
            IMongoDatabase db = MongoDbFactory.Connect(config, settings);
            var collection = db.GetCollection<Workout>(settings.WorkoutsCollectionName);
            return new Repository(collection);
        }
    }
}