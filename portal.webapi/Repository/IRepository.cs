using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.webapi.Repository
{
    public interface IRepository<T>
    {
        #region Properties
        IMongoCollection<T> Collection {get;set;}
        // ILogger Logger { get; set; }
        // ConfigurationService ConfigService { get; set; }
        // IWorkoutsDatabaseSettings WorkoutDbSettings { get; set; }
        // IConfiguration Configuration { get; set; }
        #endregion
        Task<T> InsertOneAsync(T workout);
        Task<T> GetOneByIdAsync(string id);
        Task<List<T>> GetLatestAsync(int limit);
        Task<DeleteResult> DeleteRecordAsync(string id);
        Task<T> GetDeviceWorkout(string id);

    }
}