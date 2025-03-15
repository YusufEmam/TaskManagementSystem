namespace TaskManagementSystem.Models.DTOs.NotificationDTO
{
    public class CreateNotificationDTO
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InstructorId { get; set; }
    }
}
