using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using ksp_portal.Models;
using Microsoft.Extensions.Configuration;

namespace ksp_portal.Services
{
    public class WorkoutService
    {
        private IMongoCollection<Workout> _workouts { get; set; }
        private MongoClient _client { get; set; }
        private IMongoDatabase _database { get; set; }
        private IConfiguration _configuration { get; set; }
        private string _connectionString { get; set; }
        private IWorkoutsDatabaseSettings _workoutDbSettings;
        private ConfigurationService _configurationService;
        public WorkoutService(IConfiguration config, IWorkoutsDatabaseSettings workoutSettings)
        {
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
        }
        // Replaces the connection string with the values passed in from the env vars or user secrets.
        // Example connection string: mongodb+srv://[[MONGODBUSER]]:[[MONGODBPASSWORD]]@[[MONGODBHOSTNAME]]
        public string ModifyMongoConnectionString()
        {
            _configurationService = new ConfigurationService(_workoutDbSettings, _configuration);
            return _configurationService.ConnectionStringBuilder();
        }

        public void SetCollection()
        {
            _workouts = _database.GetCollection<Workout>(_workoutDbSettings.WorkoutsCollectionName);
        }

        public List<Workout> Get()
        {
            SetCollection();
            return _workouts.Find(workout => workout.workout_name.Equals("Adult")).ToList();
        }
    }
}