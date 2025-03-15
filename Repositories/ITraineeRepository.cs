using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public interface ITraineeRepository
    {
        Task<List<TraineeTask>> GetTasksForTraineeAsync(int traineeId);
        Task<Trainee?> GetByAccountIdAsync(string accountId);
        Task<Trainee> GetByIdAsync(int id);
        Task<List<Trainee>> GetAllAsync();
        Task<Trainee> CreateAsync(Trainee trainee);
        Task<Trainee> UpdateAsync(Trainee trainee);
        Task<Trainee> DeleteAsync(int id);
    }
}
