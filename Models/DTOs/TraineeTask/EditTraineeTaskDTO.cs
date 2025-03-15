using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.TraineeTask
{
    public class EditTraineeTaskDTO
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DisplayName("Trainee")]
        public int TraineeId { get; set; }

        [DisplayName("Task")]
        public int TaskId { get; set; }
    }
}
