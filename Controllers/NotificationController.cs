using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Mapping;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.NotificationDTO;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public IMapper _mapper { get; }
        public INotificationRepository _notificationRepository { get; }

        public NotificationController(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDTO createNotificationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notification = _mapper.Map<Notification>(createNotificationDTO);

            notification = await _notificationRepository.CreateAsync(notification);

            var notificationDto = notification.NotificationInfo();

            return Ok(notificationDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationRepository.GetAllAsync();

            var notificationsDto = notifications.NotificationsInfo();

            return Ok(notificationsDto);
        }
    }
}
