using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public interface IInsturctorRepository
    {
        Task<Instructor?> GetByAccountIdAsync(string accountId);
        Task<Instructor> GetByIdAsync(int id);
        Task<List<Instructor>> GetAllAsync();
        Task<Instructor> CreateAsync(Instructor instructor);
        Task<Instructor> UpdateAsync(Instructor instructor);
        Task<Instructor> DeleteAsync(int id);
    }
}
