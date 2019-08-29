using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using portal.webapi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace portal.webapi.Services
{
    public class WorkoutService
    {
        private IMongoCollection<Workout> _workouts { get; set; }
    
        private MongoClient _client { get; set; }
        private IMongoDatabase _database { get; set; }
        private IConfiguration _configuration { get; set; }
        private string _connectionString { get; set; }
        private IWorkoutsDatabaseSettings _workoutDbSettings;
        private readonly ILogger _logger;
        private ConfigurationService _configurationService;
        public WorkoutService(IConfiguration config, IWorkoutsDatabaseSettings workoutSettings, ILoggerFactory logerFactory)
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
            _workouts = _database.GetCollection<Workout>(_workoutDbSettings.WorkoutsCollectionName);
        }
        // Replaces the connection string with the values passed in from the env vars or user secrets.
        // Example connection string: mongodb+srv://[[MONGODBUSER]]:[[MONGODBPASSWORD]]@[[MONGODBHOSTNAME]]
        public string ModifyMongoConnectionString()
        {
            _configurationService = new ConfigurationService(_workoutDbSettings, _configuration);
            return _configurationService.ConnectionStringBuilder();
        }

        public List<Workout> Get()
        {
            var foundWorkout = _workouts.Find(workout => workout.workout_name.Equals("Adult")).ToList();
            foundWorkout.ForEach(x => {
                _logger.LogInformation($"User Queried workout: {x.id}");
            });
            return foundWorkout;
        }

        #region  Inserts
        public async void InsertOneWorkout(Workout workout)
        {
            await _workouts.InsertOneAsync(workout);
        }
        #endregion

        #region  Deletes
        //public async void 
        #endregion
    }
}