using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.TraineeDTO
{
    public class EditTraineeDTO
    {
        [Required]
        [Length(1, 50)]
        [RegularExpression(@"^[A-Za-z0-9]+(?: [A-Za-z0-9]+)*$", ErrorMessage = "Name must contain only letters and numbers")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current Password")]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string? NewPassword { get; set; }

        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm New Password")]
        public string? ConfirmNewPassword { get; set; }
    }
}
