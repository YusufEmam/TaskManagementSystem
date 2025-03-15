using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Domains
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Length(1,30)]
        public string Message { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
