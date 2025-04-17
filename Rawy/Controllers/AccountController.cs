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
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest();

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

      [Authorize(Roles = "Admin")]
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
    }
}
