using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using portal.webapi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace portal.webapi.Repository
{
    public class WorkoutRepository : RepositoryBase<Workout>
    {
        public WorkoutRepository(IConfiguration config, IWorkoutsDatabaseSettings workoutSettings, ILoggerFactory loggerFactory) : base(config, workoutSettings, loggerFactory)
        {
        }
        public override List<Workout> Get()
        {
            var foundWorkout = base.Collection.Find(workout => workout.workout_name.Equals("Adult")).ToList();
            foundWorkout.ForEach(x => {
                base._logger.LogInformation($"User Queried workout: {x.id}");
            });
            return foundWorkout;
        }

        #region  Inserts
        public override async Task<Workout> InsertOneAsync(Workout workout)
        {
            await base.Collection.InsertOneAsync(workout);
            return workout;
        }
        #endregion

        #region  Deletes
        //public async void 
        #endregion

        #region Retrieve
        public override async Task<Workout> GetOneByIdAsync(string id)
        {
            FilterDefinition<Workout> filter =  Builders<Workout>.Filter.Eq("_id", ObjectId.Parse(id));
            Workout workout = await base.Collection.Find(filter).FirstAsync();
            return workout;
        }
        /// <summary>
        /// Returns a list of the latest amount of workouts based on the limit number passed in.
        /// </summary>
        /// <returns></returns>
        public override async Task<List<Workout>> GetLatestAsync(int limit)
        {
            FilterDefinition<Workout> filter = Builders<Workout>.Filter.Exists("_id");
            var sort = Builders<Workout>.Sort.Descending("workout_date");
            return await base.Collection.Find(filter).Sort(sort).Limit(limit).ToListAsync();
        }
        #endregion
    }
}