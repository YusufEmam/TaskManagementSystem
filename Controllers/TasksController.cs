using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Mapping;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.NotificationDTO;
using TaskManagementSystem.Models.DTOs.TaskDTO;
using TaskManagementSystem.Models.DTOs.TasksDTO;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITraineeRepository _traineeRepository;
        private readonly IInsturctorRepository _insturctorRepository;
        private readonly ITraineeTaskRepository _traineeTaskRepository;
        private readonly HttpClient _httpClient;

        public TasksController(ITaskRepository taskRepository, ITraineeRepository traineeRepository,
            IInsturctorRepository insturctorRepository, ITraineeTaskRepository traineeTaskRepository, HttpClient httpClient)
        {
            _taskRepository = taskRepository;
            _traineeRepository = traineeRepository;
            _insturctorRepository = insturctorRepository;
            _traineeTaskRepository = traineeTaskRepository;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("GetTasks")]
        [Authorize]
        public async Task<IActionResult> GetTasksPagePagination(int page, int size)
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            if (page < 1 || size < 1)
            {
                return BadRequest("Page and size must be greater than 0.");
            }

            var total = await _taskRepository.GetAllCountAsync(instId);

            var totalPages = (int)Math.Ceiling(total / (double)size);

            var tasks = await _taskRepository.GetTasksPaginatedAsync(instId, page, size);

            return Ok(new
            {
                Data = tasks.Select(t => t.TaskInfo()).ToList(),
                Pagination = new
                {
                    Total = total,
                    Page = page,
                    Size = size,
                    TotalPages = totalPages,
                    Next = page < totalPages ? $"/api/Tasks/GetTasks?page={page + 1}&size={size}" : null
                }
            });
        }

        [HttpGet]
        [Route("GetByID/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByID(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskDTO = task.TaskInfo();

            return Ok(taskDTO);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> Create([FromBody] CreateTasksDTO createTaskDTO)
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            if (instId != createTaskDTO.InstructorId)
            {
                return Unauthorized("You can't create a task for another instructor!");
            }

            if (createTaskDTO == null)
            {
                return BadRequest();
            }

            var newTask = createTaskDTO.CreateTask();
            if (newTask == null)
            {
                return BadRequest();
            }

            var instructor = await _insturctorRepository.GetByIdAsync(createTaskDTO.InstructorId);
            if (instructor == null)
            {
                return NotFound("Instructor not found.");
            }

            if (instructor.Tasks == null)
            {
                instructor.Tasks = new List<Tasks>();
            }

            instructor.Tasks.Add(newTask);

            await _taskRepository.CreateAsync(newTask);

            var taskDTO = newTask.TaskInfo();

            //Notification
            var notificationDto = new CreateNotificationDTO
            {
                Message = $"{newTask.Instructor.Name} created Task #{taskDTO.Id}",
                CreatedAt = DateTime.UtcNow,
                InstructorId = taskDTO.InstructorId
            };

            //Send request
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5018/api/Notification", notificationDto);

            return Ok(new { Id = taskDTO.Id, Title = taskDTO.Title, Description = taskDTO.Description, InstructorId = taskDTO.InstructorId });
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> Update([FromBody] EditTasksDTO editTasksDTO)
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            if (instId != editTasksDTO.InstructorId)
            {
                return Unauthorized("You can't edit a task for another instructor!");
            }

            var existingTask = await _taskRepository.GetByIdAsync(editTasksDTO.Id);

            if (existingTask == null)
            {
                return NotFound();
            }

            var editedTask = editTasksDTO.UpdateTask();
            await _taskRepository.UpdateAsync(editedTask);
            return Ok(editedTask);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> DeleteTasks(int instructorId, int taskId)
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            if (instId != instructorId)
            {
                return Unauthorized("You can't delete a task for another instructor!");
            }

            var existingTask = await _taskRepository.GetByIdAsync(taskId);

            if (existingTask == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteAsync(taskId);
            return Ok("Task deleted successfully.");
        }
    }
}
