using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Domains
{
    public class TraineeTask
    {
        public bool IsDeleted { get; set; } = false;

        [DisplayName("Trainee")]
        public int TraineeId { get; set; }
        public virtual Trainee? Trainee { get; set; }

        [DisplayName("Task")]
        public int TaskId { get; set; }
        public virtual Tasks? Task { get; set; }

        public TaskCompletionStatus Status { get; set; }
    }
}
