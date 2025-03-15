using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Models.DTOs.TraineeDTO
{
    public class TraineeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
