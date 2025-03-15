using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Domains
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Length(1, 30)]
        public string Title { get; set; }

        [Required]
        [Length(1, 255)]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(TaskCompletionStatus))]
        public TaskCompletionStatus Status { get; set; } = TaskCompletionStatus.InProgress;

        public bool IsDeleted { get; set; } = false;

        [DisplayName("Instructor")]
        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<TraineeTask>? TraineeTasks { get; set; }
    }

    public enum TaskCompletionStatus
    {
        InProgress = 0,
        Completed = 1
    }
}
