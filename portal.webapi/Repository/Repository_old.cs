using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using portal.webapi.Models;
using portal.webapi.Services;

namespace portal.webapi.Repository
{

    public abstract class RepositoryBase_old
    {
        // private abstract IMongoCollection<TEntity> Collection { get; set; }
        protected MongoClient _client { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IConfiguration  Configuration { get; set; }
        protected IWorkoutsDatabaseSettings WorkoutDbSettings { get; set; }
        protected ILogger Logger { get; set; }
        protected ConfigurationService ConfigurationService { get; set; }
        public RepositoryBase_old(IConfiguration config, IWorkoutsDatabaseSettings workoutSettings, ILoggerFactory logerFactory)
        {
            // Create logger from factory for the WorkoutService
            Logger = logerFactory.CreateLogger("portal.webapi.Services.WorkoutService");
            Configuration = config;
            WorkoutDbSettings = workoutSettings;
            // Get the type of IWorkoutDatabaseSettings name for use in .GetSection
            string settingsObjectName = WorkoutDbSettings.GetType().ToString().Split('.').Last();
            // Then bind it to the IWorkoutDatabaseSettings object
            Configuration.GetSection(settingsObjectName).Bind(WorkoutDbSettings);
            // Init connection string and connect to db
            WorkoutDbSettings.ConnectionString = ModifyMongoConnectionString();
            _client = new MongoClient(WorkoutDbSettings.ConnectionString);
            Database = _client.GetDatabase(WorkoutDbSettings.DatabaseName);
            // Set the collection
            // Collection = _database.GetCollection<TEntity>(_workoutDbSettings.WorkoutsCollectionName);
        }
        // Replaces the connection string with the values passed in from the env vars or user secrets.
        // Example connection string: mongodb+srv://[[MONGODBUSER]]:[[MONGODBPASSWORD]]@[[MONGODBHOSTNAME]]
        public string ModifyMongoConnectionString()
        {
            ConfigurationService = new ConfigurationService(WorkoutDbSettings, Configuration);
            return ConfigurationService.ConnectionStringBuilder();
        }

        public abstract List<TEntity> Get<TEntity>() where TEntity : Workout;

        #region  Inserts
        public abstract Task<TEntity> InsertOneAsync<TEntity>(TEntity workout);
        #endregion

        #region  Deletes
        //public async void 
        #endregion

        #region Retrieve
        public abstract Task<TEntity> GetOneByIdAsync<TEntity>(string id);
        /// <summary>
        /// Returns a list of the latest amount of workouts based on the limit number passed in.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<TEntity>> GetLatestAsync<TEntity>(int limit);
        #endregion

    }

//     public interface IRepository 
//     {
//         List< 
//     }
}