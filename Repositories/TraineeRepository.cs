using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public class TraineeRepository : ITraineeRepository
    {
        public AppDbContext _context { get; }

        public TraineeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TraineeTask>> GetTasksForTraineeAsync(int traineeId)
        {
            return await _context.TraineeTasks
                .Include(tt => tt.Task)
                .Where(tt => tt.TraineeId == traineeId)
                .ToListAsync();
        }

        public async Task<Trainee?> GetByAccountIdAsync(string accountId)
        {
            return await _context.Trainees
                .Include(t => t.Account)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Account.Id == accountId);
        }

        public async Task<Trainee> GetByIdAsync(int id)
        {
            return await _context.Trainees
                .Include(t => t.Account)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Trainee>> GetAllAsync()
        {
            return await _context.Trainees
                .Include(t => t.Account)
                .Where(t => t.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Trainee> CreateAsync(Trainee trainee)
        {
            await _context.Trainees.AddAsync(trainee);
            await _context.SaveChangesAsync();
            return trainee;
        }

        public async Task<Trainee> UpdateAsync(Trainee trainee)
        {
            var existingTrainee = await _context.Trainees
                .Include(t => t.Account)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == trainee.Id);

            if (existingTrainee == null)
            {
                return null;
            }

            existingTrainee.Name = trainee.Name;
            existingTrainee.IsDeleted = trainee.IsDeleted;

            await _context.SaveChangesAsync();
            return existingTrainee;
        }

        public async Task<Trainee> DeleteAsync(int id)
        {
            var existingTrainee = await _context.Trainees
                .Include(i => i.Account)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existingTrainee == null)
            {
                return null;
            }

            existingTrainee.IsDeleted = true;
            existingTrainee.AccountId = null;

            await _context.SaveChangesAsync();
            return existingTrainee;
        }
    }
}
