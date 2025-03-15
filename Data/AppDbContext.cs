using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext<Account>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }
        public virtual DbSet<TraineeTask> TraineeTasks { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TraineeTask>()
                .HasKey(tt => new { tt.TraineeId, tt.TaskId });

            builder.Entity<TraineeTask>()
                .HasOne(tt => tt.Trainee)
                .WithMany(t => t.TraineeTasks)
                .HasForeignKey(tt => tt.TraineeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TraineeTask>()
                .HasOne(tt => tt.Task)
                .WithMany(t => t.TraineeTasks)
                .HasForeignKey(tt => tt.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed Roles
            DbInitializer.SeedRoles(builder);

            //Seed Instructors
            DbInitializer.SeedInstructors(builder);

            //Seed Trainees
            DbInitializer.SeedTrainees(builder);
        }
    }
}
