using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Mapping;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.InstructorDTO;
using TaskManagementSystem.Models.DTOs.TasksDTO;
using TaskManagementSystem.Models.DTOs.TraineeDTO;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : ControllerBase
    {
        public UserManager<Account> _userManager { get; }
        public SignInManager<Account> _signInManager { get; }
        public IMapper _mapper { get; }
        public ITraineeRepository _traineeRepository { get; }

        public TraineeController(UserManager<Account> userManager, SignInManager<Account> signInManager,
            IMapper mapper, ITraineeRepository traineeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _traineeRepository = traineeRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var trainees = await _traineeRepository.GetAllAsync();
            var traineesDTO = trainees.TraineesInfo();
            return Ok(traineesDTO);
        }

        [HttpGet]
        [Route("GetByID/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByID(int id)
        {
            var trainee = await _traineeRepository.GetByIdAsync(id);

            if (trainee == null)
            {
                return NotFound();
            }

            var traineeDTO = trainee.TraineeInfo();
            return Ok(traineeDTO);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateTraineeDTO createTraineeDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(createTraineeDTO.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists!");
            }
            var account = createTraineeDTO.RegisterTrainee();

            account.EmailConfirmed = true;

            var identityResult = await _userManager.CreateAsync(account, createTraineeDTO.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(account, "Trainee");

                if (identityResult.Succeeded)
                {
                    var trainee = createTraineeDTO.CreateTrainee();

                    trainee.AccountId = account.Id;

                    trainee = await _traineeRepository.CreateAsync(trainee);

                    return Ok(new { Id = trainee.Id, Name = trainee.Name, Email = trainee.Account.Email });
                }
                return BadRequest(identityResult.Errors);
            }
            return BadRequest(identityResult.Errors);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> Update([FromBody] EditTraineeDTO editTraineeDTO)
        {
            int trnId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out trnId);

            var trainee = editTraineeDTO.UpdateTrainee();

            trainee.Id = trnId;

            trainee = await _traineeRepository.UpdateAsync(trainee);

            if (trainee == null)
            {
                return NotFound();
            }

            var existingTrainee = await _userManager.FindByIdAsync(trainee.AccountId);

            if (existingTrainee == null)
            {
                return NotFound();
            }

            existingTrainee.Email = editTraineeDTO.Email;
            existingTrainee.UserName = editTraineeDTO.Email;

            if (string.IsNullOrWhiteSpace(editTraineeDTO.CurrentPassword) &&
                            !string.IsNullOrWhiteSpace(editTraineeDTO.NewPassword))
            {
                return BadRequest("You must enter your current password to set a new one.");
            }

            if (!string.IsNullOrWhiteSpace(editTraineeDTO.CurrentPassword) &&
                        !string.IsNullOrWhiteSpace(editTraineeDTO.NewPassword))
            {
                bool isCurrentPasswordValid = await _userManager.CheckPasswordAsync(existingTrainee, editTraineeDTO.CurrentPassword);

                if (!isCurrentPasswordValid)
                {
                    return BadRequest("Current password is incorrect.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(existingTrainee);
                var passwordResult = await _userManager.ResetPasswordAsync(existingTrainee, token, editTraineeDTO.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors);
                }
            }

            var updateResult = await _userManager.UpdateAsync(existingTrainee);

            if (updateResult.Succeeded)
            {
                await _signInManager.SignOutAsync();

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Role", "Trainee"));
                claims.Add(new Claim("UserId", trainee.Id.ToString()));

                await _signInManager.SignInWithClaimsAsync(existingTrainee, true, claims);

                return Ok(new { Id = trainee.Id, Name = trainee.Name, Email = trainee.Account.Email });
            }
            return BadRequest(updateResult.Errors);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> Delete()
        {
            int trnId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out trnId);

            var existingTrainee = await _traineeRepository.GetByIdAsync(trnId);

            if (existingTrainee == null)
            {
                return NotFound("Trainee not found!");
            }
            else
            {
                var deleteResult = await _userManager.DeleteAsync(existingTrainee.Account);
                if (!deleteResult.Succeeded)
                {
                    return BadRequest(deleteResult.Errors);
                }
                else
                {
                    existingTrainee = await _traineeRepository.DeleteAsync(trnId);
                    return Ok("Trainee deleted successfully!");
                }
            }
        }
    }
}
