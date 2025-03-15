using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetTasksPaginatedAsync(int instructorId, int page, int size);
        Task<int> GetAllCountAsync(int instructorId);
        Task<Tasks> GetByIdAsync(int id);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<Tasks> DeleteAsync(int id);
    }
}
