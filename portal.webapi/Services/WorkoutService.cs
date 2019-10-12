using Microsoft.Extensions.Configuration;
using portal.webapi.Repository;
using portal.webapi.Models;

namespace portal.webapi.Services
{
    public class WorkoutService
    {
        protected IRepositoryFactory _repositoryFactory { get; set; }
        private IConfiguration _configuration {get;set;}
        private WorkoutsDatabaseSettings _settings {get;set;}

        public WorkoutService(IMongoDatabaseFactory mongoDbFactory, IConfiguration configuration, WorkoutsDatabaseSettings settings)
        {
            _repositoryFactory = new RepositoryFactory(mongoDbFactory);
            _configuration = configuration;
            _settings = settings;
        }

        public IRepository<Workout> GetRepository()
        {
            return _repositoryFactory.Create<Workout>(_configuration, _settings);
        }
    }
}