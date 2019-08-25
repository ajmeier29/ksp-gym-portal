

namespace ksp_portal.Models
{
    public class WorkoutsDatabaseSettings : IWorkoutsDatabaseSettings
    {
        public string WorkoutsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DbUserEnvVarName { get; set; }
        public string DbUserPassEnvVarName { get; set; }
        public string DbHostNameEnvVarName { get; set; }
    }
    public interface IWorkoutsDatabaseSettings
    {
        string WorkoutsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string DbUserEnvVarName { get; set; }
        string DbUserPassEnvVarName { get; set; }
        string DbHostNameEnvVarName { get; set; }
    }
}