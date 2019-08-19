

namespace ksp_portal.Models
{
    public class WorkoutsDatabaseSettings : IWorkoutsDatabaseSettings
    {
        public string WorkoutsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IWorkoutsDatabaseSettings
    {
        string WorkoutsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}