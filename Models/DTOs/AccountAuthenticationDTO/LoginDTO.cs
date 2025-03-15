using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.DTOs.AccountDTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
