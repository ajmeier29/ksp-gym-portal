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
    public class Repository<T> : IRepository<T>
    {
        // public ILogger Logger { get; set; }
        public IMongoCollection<T> Collection { get; set; }
        public Repository(IMongoCollection<T> collection)
        {
            if(collection == null){
                throw new InvalidOperationException("IMongoCollection<T> Collection is null in Repository object!");
            }
            Collection = collection;
            // Logger = loggerFactory.CreateLogger("portal.webapi.Services.WorkoutService");;
        }
        
        // public List<T> Get(IFindFluent<T,T> filterSearch)
        // {
        //     var foundWorkout = Collection.Find(workout => workout.workout_name.Equals("Adult")).ToList();
        //     foundWorkout.ForEach(x =>
        //     {
        //         base.Logger.LogInformation($"User Queried workout: {x.id}");
        //     });
        //     return foundWorkout;
        // }

        #region  Inserts
        public async Task<T> InsertOneAsync(T model)
        {
            await Collection.InsertOneAsync(model);
            return model;
        }
        #endregion

        #region  Deletes
        //public async void 
        #endregion

        #region Retrieve
        
        public async Task<List<T>> GetOne(FilterDefinition<T> filter) 
        {
            throw new NotImplementedException();
        }
        public async Task<T> GetOneByIdAsync(string id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            T model = await Collection.Find(filter).FirstOrDefaultAsync();
            return model;
        }

        public async Task<DeleteResult> DeleteRecordAsync(string id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            Task<DeleteResult> deleteResult  = Collection.DeleteOneAsync(filter);
            return deleteResult.Result;
        }
        /// <summary>
        /// Returns a list of the latest amount of workouts based on the limit number passed in.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetLatestAsync(int limit)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Exists("_id");
            var sort = Builders<T>.Sort.Descending("workout_date");
            return await Collection.Find(filter).Sort(sort).Limit(limit).ToListAsync();
        }
        #endregion
    }
}