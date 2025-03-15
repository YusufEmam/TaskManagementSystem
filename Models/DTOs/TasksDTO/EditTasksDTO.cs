using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.TaskDTO
{
    public class EditTasksDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Length(1, 30)]
        public string Title { get; set; }

        [Required]
        [Length(1, 255)]
        public string Description { get; set; }
        public int InstructorId { get; set; }
    }
}
