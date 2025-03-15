using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.TasksDTO
{
    public class UpdateTaskStatusDTO
    {
        public int TaskId { get; set; }
        public int TraineeId { get; set; }
        public TaskCompletionStatus Status { get; set; }
    }
}
