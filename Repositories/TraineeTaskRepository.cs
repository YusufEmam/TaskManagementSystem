using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public class TraineeTaskRepository : ITraineeTaskRepository
    {
        public AppDbContext _context { get; }

        public TraineeTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AssignTaskToTraineeAsync(TraineeTask traineeTask)
        {
            _context.TraineeTasks.Add(traineeTask);
            await _context.SaveChangesAsync();
        }

        public async Task<TraineeTask> GetTraineeTaskAsync(int traineeId, int taskId)
        {
            return await _context.TraineeTasks
                .FirstOrDefaultAsync(tt => tt.TraineeId == traineeId && tt.TaskId == taskId);
        }

        public async Task UpdateTraineeTaskAsync(TraineeTask traineeTask)
        {
            _context.TraineeTasks.Update(traineeTask);
            await _context.SaveChangesAsync();
        }

        public async Task<TraineeTask?> GetByIdAsync(int traineeId, int taskId)
        {
            return await _context.TraineeTasks
                .Include(tt => tt.Trainee)
                .Include(tt => tt.Task)
                .Where(tt => tt.TraineeId == traineeId && tt.TaskId == taskId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TraineeTask>> GetAllAsync()
        {
            return await _context.TraineeTasks
                .Include(tt => tt.Trainee)
                .Include(tt => tt.Task)
                .Where(tt => tt.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Trainee?>?> GetAllTraineesByTaskAsync(int taskId)
        {
            if (!await _context.Tasks.AnyAsync(t => t.Id == taskId))
                return null;

            return await _context.TraineeTasks
                .Include(tt => tt.Trainee)
                .Where(tt => tt.TaskId == taskId && tt.IsDeleted == false)
                .Select(tt => tt.Trainee).ToListAsync();
        }

        public async Task<List<TraineeTask?>?> GetAllTasksByTraineeAsync(int instructorId, int traineeId)
        {
            if (!await _context.Trainees.AnyAsync(t => t.Id == traineeId))
                return null;

            return await _context.TraineeTasks
                .Include(tt => tt.Task)
                .Where(tt => tt.TraineeId == traineeId && tt.IsDeleted == false && tt.Task.IsDeleted == false && tt.Task.InstructorId == instructorId)
                .ToListAsync();
        }

        public async Task CreateAsync(TraineeTask traineeTask)
        {
            await _context.TraineeTasks.AddAsync(traineeTask);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int traineeId, int taskId, bool isDeleted)
        {
            var traineeTask = await _context.TraineeTasks
            .FirstOrDefaultAsync(tt => tt.TraineeId == traineeId && tt.TaskId == taskId);

            if (traineeTask != null)
            {
                traineeTask.IsDeleted = isDeleted;

                await _context.SaveChangesAsync();
            }
        }
    }
}
