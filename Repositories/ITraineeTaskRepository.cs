using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public interface ITraineeTaskRepository
    {
        Task AssignTaskToTraineeAsync(TraineeTask traineeTask);
        Task<TraineeTask> GetTraineeTaskAsync(int traineeId, int taskId);
        Task UpdateTraineeTaskAsync(TraineeTask traineeTask);
        Task<List<TraineeTask?>?> GetAllTasksByTraineeAsync(int instructorId, int traineeId);
        Task<TraineeTask?> GetByIdAsync(int traineeId, int taskId);
        Task<List<TraineeTask>> GetAllAsync();
        Task<List<Trainee?>?> GetAllTraineesByTaskAsync(int taskId);
        Task CreateAsync(TraineeTask task);
        Task UpdateAsync(int traineeId, int taskId, bool isDeleted);
    }
}
