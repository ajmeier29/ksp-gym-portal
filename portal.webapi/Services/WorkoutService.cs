using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using ksp_portal.Models;
using Microsoft.Extensions.Configuration;

namespace ksp_portal.Services
{
    public class WorkoutService
    {
        private IWorkoutsDatabaseSettings _workoutDbSettings { get; set; }
        private IMongoCollection<Workout> _workouts { get; set; }
        private MongoClient _client { get; set; }
        private IMongoDatabase _database {get;set;}
        // private IConfiguration _configuration { get; set; }
        private string _connectionString { get; set; }
        public WorkoutService(IWorkoutsDatabaseSettings workoutDbSettings)
        {
            _workoutDbSettings = workoutDbSettings;
            // _configuration = config;
            _workoutDbSettings.ConnectionString = ModifyMongoConnectionString();
            Init();
        }
        public string ModifyMongoConnectionString()
        {
            _connectionString = _workoutDbSettings.ConnectionString;
            // _connectionString = _connectionString.Replace("[[MONGODBUSER]]", _configuration["MONGODBUSER"]);
            // _connectionString = _connectionString.Replace("[[MONGODBPASSWORD]]", _configuration["MONGODBPASSWORD"]);
            // _connectionString = _connectionString.Replace("[[hostname]]", _configuration["MONGODBHOSTNAME"]);
            return _connectionString;
        }
        public void Init()
        {
            _client = new MongoClient(_workoutDbSettings.ConnectionString);
            _database = _client.GetDatabase(_workoutDbSettings.DatabaseName);
        }

        public void SetCollection() {
            _workouts = _database.GetCollection<Workout>(_workoutDbSettings.WorkoutsCollectionName);
        }

        public List<Workout> Get(){
            SetCollection();
            return _workouts.Find(workout => true).ToList();
        }


    }
}