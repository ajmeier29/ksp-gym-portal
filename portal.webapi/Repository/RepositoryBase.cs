// using System.Threading.Tasks;
// using System.Linq;
// using System.Collections.Generic;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Configuration;
// using MongoDB.Driver;
// using portal.webapi.Services;
// using portal.webapi.Models;

// namespace portal.webapi.Repository
// {
//     public abstract class RepositoryBase<T> : IRepository<T>
//     {
//         public ILogger Logger { get; set; }
//         public IMongoCollection<T> Collection {get;set;}
//         public RepositoryBase(IMongoCollection<T> collection, ILoggerFactory loggerFactory)
//         {
//             // Create logger from factory for the WorkoutService
//             Logger = loggerFactory.CreateLogger("portal.webapi.Services.WorkoutService");
//             // Configuration = config;
//             // WorkoutDbSettings = workoutSettings;
//             // Get the type of IWorkoutDatabaseSettings name for use in .GetSection
//             // string settingsObjectName = WorkoutDbSettings.GetType().ToString().Split('.').Last();
//             // // Then bind it to the IWorkoutDatabaseSettings object
//             // Configuration.GetSection(settingsObjectName).Bind(WorkoutDbSettings);
//             // // Init connection string and connect to db
//             // WorkoutDbSettings.ConnectionString = ModifyMongoConnectionString();
//             // _client = new MongoClient(WorkoutDbSettings.ConnectionString);
//             // Database = _client.GetDatabase(WorkoutDbSettings.DatabaseName);
//         }
//         public abstract List<T> Get();
//         public abstract Task<T> InsertOneAsync(T model);
//         public abstract Task<T> GetOneByIdAsync(string id);
//         public abstract Task<List<T>> GetLatestAsync(int limit);
//     }
// }