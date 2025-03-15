using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskManagementSystem.Models.DTOs.InstructorDTO
{
    public class InstructorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
