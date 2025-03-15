using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.NotificationDTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InstructorId { get; set; }
    }
}
