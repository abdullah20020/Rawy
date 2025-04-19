using AutoMapper;
using core.Contracts.AuthContracts;
using core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly SignInManager<BaseUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<BaseUser> userManager,
            SignInManager<BaseUser> signInManager,
            IAuthService authService,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized();
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized();
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.UserName,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> register(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return BadRequest("Email already in use.");

            var user = new BaseUser()
            {
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
            };
    
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(); 
            await _userManager.AddToRoleAsync(user, "User");
            
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
    => await _userManager.FindByEmailAsync(email) is not null;

 
      [HttpPost("make-admin-by-id")]
        public async Task<ActionResult> MakeUserAdminById([FromBody] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded) return BadRequest("Failed to assign admin role");
            return Ok($"User with ID {userId} is now an admin.");
        }

        
        [HttpGet("all-users")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsersWithTokens()
        {
            var users = _userManager.Users.ToList();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var token = await _authService.CreateTokenAsync(user, _userManager);

                userDtos.Add(new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = token
                });
            }

            return Ok(userDtos);
        }



        [HttpPost("add-user")]
        public async Task<ActionResult<UserDto>> AddUserByAdmin(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return BadRequest("Email already in use.");

            var user = new BaseUser
            {
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest("Failed to delete user");

            return Ok($"User with ID {id} has been deleted.");
        }


    }
}
