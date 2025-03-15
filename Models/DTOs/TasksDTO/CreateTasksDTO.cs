using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.TaskDTO
{
    public class CreateTasksDTO
    {
        [Required]
        [Length(1, 30)]
        public string Title { get; set; }

        [Required]
        [Length(1, 255)]
        public string Description { get; set; }

        public int InstructorId { get; set; }
    }
}
