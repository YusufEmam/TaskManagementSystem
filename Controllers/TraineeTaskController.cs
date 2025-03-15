using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Mapping;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.TaskDTO;
using TaskManagementSystem.Models.DTOs.TasksDTO;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeTaskController : ControllerBase
    {
        public ITraineeTaskRepository _traineeTaskRepository { get; }
        public ITaskRepository _taskRepository { get; }
        public ITraineeRepository _traineeRepository { get; }
        public IInsturctorRepository _insturctorRepository { get; }

        public TraineeTaskController(ITraineeTaskRepository traineeTaskRepository, ITaskRepository taskRepository,
            ITraineeRepository traineeRepository, IInsturctorRepository insturctorRepository)
        {
            _traineeTaskRepository = traineeTaskRepository;
            _taskRepository = taskRepository;
            _traineeRepository = traineeRepository;
            _insturctorRepository = insturctorRepository;
        }

        [HttpPost]
        [Route("AssignTaskToTrainee")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> AssignTaskToTrainee([FromBody] AssignTaskDTO assignTaskDTO)
        {
            var task = await _taskRepository.GetByIdAsync(assignTaskDTO.TaskId);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            var trainee = await _traineeRepository.GetByIdAsync(assignTaskDTO.TraineeId);
            if (trainee == null)
            {
                return NotFound("Trainee not found.");
            }

            var taskAssignedAlready = await _traineeTaskRepository.GetTraineeTaskAsync(assignTaskDTO.TraineeId, assignTaskDTO.TaskId);

            if (taskAssignedAlready != null)
            {
                return BadRequest("Task already assigned to trainee.");
            }

            var traineeTask = new TraineeTask
            {
                TaskId = assignTaskDTO.TaskId,
                TraineeId = assignTaskDTO.TraineeId,
                Status = TaskCompletionStatus.InProgress
            };

            await _traineeTaskRepository.AssignTaskToTraineeAsync(traineeTask);

            return Ok("Task assigned to trainee successfully.");
        }

        [HttpPut]
        [Route("UpdateTaskStatus")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusDTO updateTaskStatusDTO)
        {
            int trnId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out trnId);

            if (trnId != updateTaskStatusDTO.TraineeId)
            {
                return Unauthorized("You are not authorized to update this task.");
            }

            var traineeTask = await _traineeTaskRepository.GetTraineeTaskAsync(updateTaskStatusDTO.TraineeId, updateTaskStatusDTO.TaskId);
            if (traineeTask == null)
            {
                return NotFound("Task/Trainee not found.");
            }

            traineeTask.Status = updateTaskStatusDTO.Status;

            await _traineeTaskRepository.UpdateTraineeTaskAsync(traineeTask);

            return Ok("Task status updated successfully.");
        }

        [HttpGet]
        [Route("GetTasksForTrainee")]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult> GetTasksForTrainee(int traineeId)
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            var traineeTasks = await _traineeTaskRepository.GetAllTasksByTraineeAsync(instId, traineeId);

            if (traineeTasks.Count() == 0)
            {
                return NotFound("No tasks found for the specified trainee.");
            }

            var taskDTOs = traineeTasks
                .Where(tt => tt.Task != null)
                .Select(tt => tt.Task.TaskInfo())
                .ToList();

            return Ok(taskDTOs);
        }
    }
}
