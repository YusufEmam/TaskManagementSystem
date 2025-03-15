using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public class InstructorRepository : IInsturctorRepository
    {
        public AppDbContext _context { get; }

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Instructor?> GetByAccountIdAsync(string accountId)
        {
            return await _context.Instructors
                .Include(i => i.Account)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Account.Id == accountId);
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors
                .Include(i => i.Account)
                .Include(i => i.Tasks.Where(t => t.IsDeleted == false))
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Instructor>> GetAllAsync()
        {
            return await _context.Instructors
                .Include(i => i.Account)
                .Where(i => i.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Instructor> CreateAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public async Task<Instructor> UpdateAsync(Instructor instructor)
        {
            var existingInstructor = await _context.Instructors
                .Include(i => i.Account)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == instructor.Id);

            if (existingInstructor == null)
            {
                return null;
            }

            existingInstructor.Name = instructor.Name;
            existingInstructor.IsDeleted = instructor.IsDeleted;

            await _context.SaveChangesAsync();
            return existingInstructor;
        }

        public async Task<Instructor> DeleteAsync(int id)
        {
            var existingInstructor = await _context.Instructors
                .Include(i => i.Account)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existingInstructor == null)
            {
                return null;
            }

            existingInstructor.IsDeleted = true;
            existingInstructor.AccountId = null;

            await _context.SaveChangesAsync();
            return existingInstructor;
        }
    }
}
