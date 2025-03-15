using AutoMapper;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.InstructorDTO;
using TaskManagementSystem.Models.DTOs.NotificationDTO;
using TaskManagementSystem.Models.DTOs.TaskDTO;
using TaskManagementSystem.Models.DTOs.TraineeDTO;

namespace TaskManagementSystem.Mapping
{
    public static class ManualMapperProfiles
    {
        // ACCOUNT
        public static Account RegisterInstructor(this CreateInstructorDTO createInstructorDTO)
        {
            return new Account
            {
                UserName = createInstructorDTO.Email,
                Email = createInstructorDTO.Email
            };
        }

        public static Account RegisterTrainee(this CreateTraineeDTO createTraineeDTO)
        {
            return new Account
            {
                UserName = createTraineeDTO.Email,
                Email = createTraineeDTO.Email
            };
        }

        // INSTRUCTOR
        public static List<InstructorDTO> InstructorsInfo(this List<Instructor> instructors)
        {
            var instructorsDTO = new List<InstructorDTO>();
            foreach (var instructor in instructors)
            {
                var instructorDTO = new InstructorDTO
                {
                    Id = instructor.Id,
                    Name = instructor.Name,
                    Email = instructor.Account.Email
                };
                instructorsDTO.Add(instructorDTO);
            }
            return instructorsDTO;
        }

        public static InstructorDTO InstructorInfo(this Instructor instructor)
        {
            var instructorDTO = new InstructorDTO
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Email = instructor.Account.Email
            };
            return instructorDTO;
        }

        public static Instructor CreateInstructor(this CreateInstructorDTO createInstructorDTO)
        {
            return new Instructor
            {
                Name = createInstructorDTO.Name
            };
        }

        public static Instructor UpdateInstructor(this EditInstructorDTO updateInstructorDTO)
        {
            return new Instructor
            {
                Name = updateInstructorDTO.Name,
                Account = new Account
                {
                    Email = updateInstructorDTO.Email,
                    PasswordHash = updateInstructorDTO.CurrentPassword,
                }
            };
        }

        //TRAINEE
        public static List<TraineeDTO> TraineesInfo(this List<Trainee> trainees)
        {
            var traineesDTO = new List<TraineeDTO>();
            foreach (var trainee in trainees)
            {
                var traineeDTO = new TraineeDTO
                {
                    Id = trainee.Id,
                    Name = trainee.Name,
                    Email = trainee.Account.Email
                };
                traineesDTO.Add(traineeDTO);
            }
            return traineesDTO;
        }

        public static TraineeDTO TraineeInfo(this Trainee trainee)
        {
            var traineeDTO = new TraineeDTO
            {
                Id = trainee.Id,
                Name = trainee.Name,
                Email = trainee.Account.Email
            };
            return traineeDTO;
        }

        public static Trainee CreateTrainee(this CreateTraineeDTO createTraineeDTO)
        {
            return new Trainee
            {
                Name = createTraineeDTO.Name
            };
        }

        public static Trainee UpdateTrainee(this EditTraineeDTO updateTraineeDTO)
        {
            return new Trainee
            {
                Name = updateTraineeDTO.Name,
                Account = new Account
                {
                    Email = updateTraineeDTO.Email,
                    PasswordHash = updateTraineeDTO.CurrentPassword,
                }
            };
        }

        //TASK
        public static List<TasksDTO> TasksInfo(this List<Tasks> tasks)
        {
            var tasksDTO = new List<TasksDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TasksDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status
                };
                tasksDTO.Add(taskDTO);
            }
            return tasksDTO;
        }

        public static TasksDTO TaskInfo(this Tasks task)
        {
            var taskDTO = new TasksDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                InstructorId = task.InstructorId
            };
            return taskDTO;
        }

        public static Tasks CreateTask(this CreateTasksDTO createTaskDTO)
        {
            return new Tasks
            {
                Title = createTaskDTO.Title,
                Description = createTaskDTO.Description,
                InstructorId = createTaskDTO.InstructorId
            };
        }

        public static Tasks UpdateTask(this EditTasksDTO updateTaskDTO)
        {
            return new Tasks
            {
                Id = updateTaskDTO.Id,
                Title = updateTaskDTO.Title,
                Description = updateTaskDTO.Description,
                InstructorId = updateTaskDTO.InstructorId
            };
        }

        //NOTIFICATION
        public static NotificationDTO NotificationInfo(this Notification notification)
        {
            var notificationDTO = new NotificationDTO
            {
                Id = notification.Id,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt,
                InstructorId = notification.InstructorId
            };
            return notificationDTO;
        }

        public static List<NotificationDTO> NotificationsInfo(this List<Notification> notifications)
        {
            var notificationsDTO = new List<NotificationDTO>();
            foreach (var notification in notifications)
            {
                var notificationDTO = new NotificationDTO
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    CreatedAt = notification.CreatedAt,
                    InstructorId = notification.InstructorId
                };
                notificationsDTO.Add(notificationDTO);
            }
            return notificationsDTO;
        }
    }
}
