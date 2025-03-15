using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Mapping;
using TaskManagementSystem.Models.Domains;
using TaskManagementSystem.Models.DTOs.AccountDTO;
using TaskManagementSystem.Repositories;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<Account> _userManager { get; }
        public SignInManager<Account> _signInManager { get; }
        public IConfiguration _configuration { get; }
        public IMapper _mapper { get; }
        public IInsturctorRepository _insturctorRepository { get; }
        public ITraineeRepository _traineeRepository { get; }

        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager,
            IConfiguration configuration, IMapper mapper, IInsturctorRepository insturctorRepository, ITraineeRepository traineeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _insturctorRepository = insturctorRepository;
            _traineeRepository = traineeRepository;
        }

        private async Task<Account> AuthenticateUser(LoginDTO user)
        {
            var userFromDb = await _userManager.FindByEmailAsync(user.Email);

            if (userFromDb != null)
            {
                bool checkPasswordResult = await _userManager.CheckPasswordAsync(userFromDb, user.Password);

                if (checkPasswordResult)
                    return userFromDb;
                else
                    return null;
            }
            else
                return null;
        }

        private string GenerateJSONWebToken(Account user, List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var account = await AuthenticateUser(loginDTO);

            if (account != null)
            {
                List<Claim> claims = new List<Claim>();
                var roles = await _userManager.GetRolesAsync(account);

                string role = "", userId = "";

                if (roles.Contains("Instructor"))
                {
                    var instructor = await _insturctorRepository.GetByAccountIdAsync(account.Id);
                    if (instructor != null)
                    {
                        role = "Instructor";
                        userId = instructor.Id.ToString();
                    }
                }
                else if (roles.Contains("Trainee"))
                {
                    var trainee = await _traineeRepository.GetByAccountIdAsync(account.Id);
                    if (trainee != null)
                    {
                        role = "Trainee";
                        userId = trainee.Id.ToString();
                    }
                }

                claims.Add(new Claim("Role", role));
                claims.Add(new Claim("UserId", userId));

                var token = GenerateJSONWebToken(account, claims);

                return Ok(new { Token = token });
            }
            else
                return Unauthorized("Invalid Credentials!");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterTraineeDTO registerTraineeDTO)
        {
            var account = _mapper.Map<RegisterTraineeDTO, Account>(registerTraineeDTO);
            account.EmailConfirmed = true;
            account.UserName = registerTraineeDTO.Email;
            account.NormalizedUserName = registerTraineeDTO.Email.ToUpper();
            var identityResult = await _userManager.CreateAsync(account, registerTraineeDTO.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(account, "Trainee");

                if (identityResult.Succeeded)
                {
                    var trainee = _mapper.Map<RegisterTraineeDTO, Trainee>(registerTraineeDTO);
                    trainee.AccountId = account.Id;
                    trainee = await _traineeRepository.CreateAsync(trainee);

                    return Ok(new {Message = "Account created successfully!", UserData =  new { Name = trainee.Name, Email = trainee.Account.Email } });
                }
                else
                    return BadRequest(identityResult.Errors);
            }
            else
                return BadRequest(identityResult.Errors);
        }
    }
}
