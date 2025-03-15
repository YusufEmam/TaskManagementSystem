using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Domains
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Length(1, 50)]
        [RegularExpression(@"^[A-Za-z0-9]+(?: [A-Za-z0-9]+)*$", ErrorMessage = "Name must contain only letters and numbers")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Display(Name = "Account")]
        public string? AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual ICollection<Tasks>? Tasks { get; set; }
    }
}
