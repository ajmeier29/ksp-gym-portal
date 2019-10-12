using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

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
        Task<List<T>> GetOne(FilterDefinition<T> filter);
        Task<T> InsertOneAsync(T workout);
        Task<T> GetOneByIdAsync(string id);
        Task<List<T>> GetLatestAsync(int limit);

    }
}