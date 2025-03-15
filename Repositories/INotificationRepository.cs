using TaskManagementSystem.Models.Domains;

namespace TaskManagementSystem.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<List<Notification>> GetAllAsync();
    }
}
