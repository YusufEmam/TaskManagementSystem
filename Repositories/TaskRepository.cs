using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public AppDbContext _context { get; }

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tasks>> GetTasksPaginatedAsync(int instructorId, int page, int size)
        {
            return await _context.Tasks
                .Where(t => t.IsDeleted == false && t.InstructorId == instructorId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetAllCountAsync(int instructorId)
        {
             return await _context.Tasks
                .Where(t => t.IsDeleted == false && t.Instructor.Id == instructorId)
                .CountAsync();
        }

        public async Task<Tasks> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Instructor)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        //public async Task<List<Tasks>> GetAllAsync()
        //{
        //    return await _context.Tasks
        //        .Include(t => t.Instructor)
        //        .Where(t => t.IsDeleted == false)
        //        .ToListAsync();
        //}

        public async Task<Tasks> CreateAsync(Tasks task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Tasks> UpdateAsync(Tasks task)
        {
            var existingTask = await _context.Tasks
                .Include(t => t.Instructor)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == task.Id);

            if (existingTask == null)
            {
                return null;
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.IsDeleted = task.IsDeleted;
            existingTask.InstructorId = task.InstructorId;

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<Tasks> DeleteAsync(int id)
        {
            var existingTask = await _context.Tasks
                .Include(t => t.Instructor)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTask == null)
            {
                return null;
            }

            existingTask.IsDeleted = true;

            await _context.SaveChangesAsync();
            return existingTask;
        }
    }
}
