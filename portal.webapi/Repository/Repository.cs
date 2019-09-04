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

    public abstract class RepositoryBase
    {
        protected IMongoCollection<TEntity> Collection { get; set; }
        protected MongoClient _client { get; set; }
        protected IMongoDatabase _database { get; set; }
        protected IConfiguration _configuration { get; set; }
        protected string _connectionString { get; set; }
        protected IWorkoutsDatabaseSettings _workoutDbSettings { get; set; }
        protected ILogger _logger { get; set; }
        protected ConfigurationService _configurationService { get; set; }
        public RepositoryBase(IConfiguration config, IWorkoutsDatabaseSettings workoutSettings, ILoggerFactory logerFactory)
        {
            // Create logger from factory for the WorkoutService
            _logger = logerFactory.CreateLogger("portal.webapi.Services.WorkoutService");
            _configuration = config;
            _workoutDbSettings = workoutSettings;
            // Get the type of IWorkoutDatabaseSettings name for use in .GetSection
            string settingsObjectName = _workoutDbSettings.GetType().ToString().Split('.').Last();
            // Then bind it to the IWorkoutDatabaseSettings object
            _configuration.GetSection(settingsObjectName).Bind(_workoutDbSettings);
            // Init connection string and connect to db
            _workoutDbSettings.ConnectionString = ModifyMongoConnectionString();
            _client = new MongoClient(_workoutDbSettings.ConnectionString);
            _database = _client.GetDatabase(_workoutDbSettings.DatabaseName);
            // Set the collection
            Collection = _database.GetCollection<TEntity>(_workoutDbSettings.WorkoutsCollectionName);
        }
        // Replaces the connection string with the values passed in from the env vars or user secrets.
        // Example connection string: mongodb+srv://[[MONGODBUSER]]:[[MONGODBPASSWORD]]@[[MONGODBHOSTNAME]]
        public string ModifyMongoConnectionString()
        {
            _configurationService = new ConfigurationService(_workoutDbSettings, _configuration);
            return _configurationService.ConnectionStringBuilder();
        }

        public abstract List<TEntity> Get<TEntity>();

        #region  Inserts
        public abstract Task<TEntity> InsertOneAsync(TEntity workout);
        #endregion

        #region  Deletes
        //public async void 
        #endregion

        #region Retrieve
        public abstract Task<TEntity> GetOneByIdAsync(string id);
        /// <summary>
        /// Returns a list of the latest amount of workouts based on the limit number passed in.
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<TEntity>> GetLatestAsync(int limit);
        #endregion

    }
}