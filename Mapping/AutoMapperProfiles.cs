using AutoMapper;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.AccountDTO;
using TaskManagementSystem.Models.DTOs.NotificationDTO;

namespace TaskManagementSystem.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterTraineeDTO, Account>().ReverseMap();
            CreateMap<RegisterTraineeDTO, Trainee>().ReverseMap();
            CreateMap<CreateNotificationDTO, Notification>().ReverseMap();
        }
    }
}
