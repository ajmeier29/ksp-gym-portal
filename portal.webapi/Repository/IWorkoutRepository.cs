using MongoDB.Driver;
using System.Collections.Generic;
using portal.webapi.Models;
using System.Threading.Tasks;

namespace portal.webapi.Repository
{
    public interface IWorkoutRepository
    {
        #region Properties
        IMongoCollection<Workout> Collection {get;set;}
        #endregion

        #region Methods
        Task<Workout> InsertOneAsync(Workout workout);
        Task<Workout> GetOneByIdAsync(string id);
        Task<List<Workout>> GetLatestAsync(int limit);
        Task<DeleteResult> DeleteRecordAsync(string id);
        Task<Workout> GetDeviceWorkout(string id);
        #endregion
    }
}