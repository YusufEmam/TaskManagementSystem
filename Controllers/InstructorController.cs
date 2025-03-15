using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using TaskManagementSystem.Mapping;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.InstructorDTO;
using TaskManagementSystem.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        public UserManager<Account> _userManager { get; }
        public SignInManager<Account> _signInManager { get; }
        public IMapper _mapper { get; }
        public IInsturctorRepository _insturctorRepository { get; }

        public InstructorController(UserManager<Account> userManager, SignInManager<Account> signInManager,
            IMapper mapper, IInsturctorRepository insturctorRepository)
        {
            _insturctorRepository = insturctorRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _insturctorRepository.GetAllAsync();
            var instructorsDTO = instructors.InstructorsInfo();
            return Ok(instructorsDTO);
        }

        [HttpGet]
        [Route("GetByID/{id:int}")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> GetByID(int id)
        {
            var instructor = await _insturctorRepository.GetByIdAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            var instructorDTO = instructor.InstructorInfo();
            return Ok(instructorDTO);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]CreateInstructorDTO createInstructorDTO)
        {
            var existingUser = await _userManager.FindByEmailAsync(createInstructorDTO.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists!");
            }

            var account = createInstructorDTO.RegisterInstructor();

            account.EmailConfirmed = true;

            var identityResult = await _userManager.CreateAsync(account, createInstructorDTO.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(account, "Instructor");

                if (identityResult.Succeeded)
                {
                    var instructor = createInstructorDTO.CreateInstructor();

                    instructor.AccountId = account.Id;

                    instructor = await _insturctorRepository.CreateAsync(instructor);

                    return Ok(new { Id = instructor.Id, Name = instructor.Name, Email = instructor.Account.Email });
                }
                return BadRequest(identityResult.Errors);
            }
            return BadRequest(identityResult.Errors);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Update([FromBody]EditInstructorDTO editInstructorDTO)
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            var instructor = editInstructorDTO.UpdateInstructor();

            instructor.Id = instId;

            instructor = await _insturctorRepository.UpdateAsync(instructor);

            if (instructor == null)
            {
                return NotFound();
            }

            var existingInstructor = await _userManager.FindByIdAsync(instructor.AccountId);

            if (existingInstructor == null)
            {
                return NotFound();
            }

            existingInstructor.Email = editInstructorDTO.Email;

            if (string.IsNullOrWhiteSpace(editInstructorDTO.CurrentPassword) &&
                            !string.IsNullOrWhiteSpace(editInstructorDTO.NewPassword))
            {
                return BadRequest("You must enter your current password to set a new one.");
            }

            if (!string.IsNullOrWhiteSpace(editInstructorDTO.CurrentPassword) &&
                        !string.IsNullOrWhiteSpace(editInstructorDTO.NewPassword))
            {
                bool isCurrentPasswordValid = await _userManager.CheckPasswordAsync(existingInstructor, editInstructorDTO.CurrentPassword);

                if (!isCurrentPasswordValid)
                {
                    return BadRequest("Current password is incorrect.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(existingInstructor);
                var passwordResult = await _userManager.ResetPasswordAsync(existingInstructor, token, editInstructorDTO.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors);
                }
            }

            var updateResult = await _userManager.UpdateAsync(existingInstructor);

            if (updateResult.Succeeded)
            {
                await _signInManager.SignOutAsync();

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Role", "Instructor"));   
                claims.Add(new Claim("UserId", instructor.Id.ToString()));

                await _signInManager.SignInWithClaimsAsync(existingInstructor, true, claims);

                return Ok(new { Id = instructor.Id, Name = instructor.Name, Email = instructor.Account.Email });
            }
            return BadRequest(updateResult.Errors);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Delete()
        {
            int instId = 0;
            int.TryParse(User.FindFirst("UserId")?.Value, out instId);

            var existingInstructor = await _insturctorRepository.GetByIdAsync(instId);

            if (existingInstructor == null)
            {
                return NotFound("Instructor not found!");
            }
            else
            {
                var deleteResult = await _userManager.DeleteAsync(existingInstructor.Account);
                if (!deleteResult.Succeeded)
                {
                    return BadRequest(deleteResult.Errors);
                }
                else
                {
                    existingInstructor = await _insturctorRepository.DeleteAsync(instId);
                    return Ok("Instructor deleted successfully!");
                }
            }
        }
    }
}
