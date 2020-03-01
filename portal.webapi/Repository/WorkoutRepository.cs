using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System;
using portal.webapi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace portal.webapi.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private ILogger<WorkoutRepository> _logger { get; set; }
        public IMongoCollection<Workout> Collection { get; set; }
        public WorkoutRepository(IMongoCollection<Workout> collection, ILogger<WorkoutRepository> logger)
        {
            if(collection == null){
                throw new InvalidOperationException("IMongoCollection<Workout> Collection is null in Repository object!");
            }
            Collection = collection;
            _logger = logger;
            // Logger = loggerFactory.CreateLogger("portal.webapi.Services.WorkoutService");;
        }
        
        // public List<Workout> Get(IFindFluent<Workout,Workout> filterSearch)
        // {
        //     var foundWorkout = Collection.Find(workout => workout.workout_name.Equals("Adult")).ToList();
        //     foundWorkout.ForEach(x =>
        //     {
        //         base.Logger.LogInformation($"User Queried workout: {x.id}");
        //     });
        //     return foundWorkout;
        // }

        #region  Inserts
        public async Task<Workout> InsertOneAsync(Workout model)
        {
            await Collection.InsertOneAsync(model);
            return model;
        }
        #endregion

        #region  Deletes
        //public async void 
        #endregion

        #region Retrieve
        public async Task<Workout> GetDeviceWorkout(string id)
        {
            var filter = Builders<Workout>.Filter.ElemMatch(x => x.devices, x => x.id == id);
            // filter = filter & Builders<Workout>.Filter.ElemMatch(x => x.workout_times, x => x == new DateTime(2019, 10, 26, 18, 00, 00));
            var time = new DateTime(2019, 10, 26, 18, 00, 00);

            filter = filter & Builders<Workout>.Filter.ElemMatch(x => x.workout_times, x => x == time);
            Workout model = await Collection.Find(filter).FirstOrDefaultAsync();
            var workouttime = model.workout_times;
            return model;
        }
        public async Task<Workout> GetOneByIdAsync(string id)
        {
            FilterDefinition<Workout> filter = Builders<Workout>.Filter.Eq("_id", ObjectId.Parse(id));
            Workout model = await Collection.Find(filter).FirstOrDefaultAsync();
            return model;
        }

        public async Task<DeleteResult> DeleteRecordAsync(string id)
        {
            FilterDefinition<Workout> filter = Builders<Workout>.Filter.Eq("_id", ObjectId.Parse(id));
            Task<DeleteResult> deleteResult  = Collection.DeleteOneAsync(filter);
            return deleteResult.Result;
        }
        /// <summary>
        /// Returns a list of the latest amount of workouts based on the limit number passed in.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Workout>> GetLatestAsync(int limit)
        {
            _logger.LogInformation($"GetLatestAsync was called with a limite of {limit}");
            FilterDefinition<Workout> filter = Builders<Workout>.Filter.Exists("_id");
            var sort = Builders<Workout>.Sort.Descending("date_added");
            return await Collection.Find(filter).Sort(sort).Limit(limit).ToListAsync();
        }
        #endregion
    }
}