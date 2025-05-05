using AutoMapper;
using core.Contracts.AuthContracts;
using core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rawy.Dtos;
using Repsotiry.Data;
using System.Security.Claims;
using System.Text.Json;

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
        private readonly IMemoryCache _memoryCache;
        private readonly RawyDbcontext rawyDbcontext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<BaseUser> userManager,
            SignInManager<BaseUser> signInManager,
            IAuthService authService,
            IMapper mapper, IMemoryCache memoryCache,
            RawyDbcontext rawyDbcontext,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            this.rawyDbcontext = rawyDbcontext;
            _roleManager = roleManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized();


            List<int> recommendedBookIds = await GetRecommendationsFromFlask(user.Id);

            if (recommendedBookIds != null && recommendedBookIds.Any())
            {
                _memoryCache.Set(user.Id, recommendedBookIds, TimeSpan.FromHours(1));
            }

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

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var user = await rawyDbcontext.Set<BaseUser>()
                .Include(u => u.Reviews)
                .Include(u => u.Favorites)
                .Include(u => u.Records)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("User not found");

            return Ok(new showuserdto
            {
                Id = user.Id,
                email = user.Email,
                records = user.Records,
                DisplayName = user.UserName,
                ProfilePicture = user.ProfilePicture,
                DateJoined = user.DateJoined,
                Cv_Url = user.Cv_Url,
                ReviewsCount = user.Reviews?.Count ?? 0,
                favoriteCount = user.Favorites?.Count ?? 0
            });
        }
        [HttpGet]
        [Authorize] 
        public async Task<ActionResult<UserAccountDto>> UserAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            var user = await rawyDbcontext.Set<BaseUser>()
                .Include(u => u.Reviews)
                .Include(u => u.Favorites)
                .Include(u => u.Records)
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found");

            return Ok(new UserAccountDto
            {
                
                email = user.Email,
                DisplayName = user.UserName,
                ProfilePicture = user.ProfilePicture,
                DateJoined = user.DateJoined,
                Cv_Url = user.Cv_Url,
                Playlists = user.Playlists,     
                Favorites = user.Favorites, 
                Records = user.Records,
                Reviews = user.Reviews
    
            });
        }

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

        private async Task<List<int>?> GetRecommendationsFromFlask(string userId)
        {
            try
            {
                using var httpClient = new HttpClient();
                string flaskUrl = $"http://localhost:5000/recommendations?user_id={userId}";

                var response = await httpClient.GetAsync(flaskUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);  // Debugging the raw JSON
                var flaskResponse = JsonSerializer.Deserialize<FlaskResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (flaskResponse?.Recommendations == null || !flaskResponse.Recommendations.Any())
                {
                    return new List<int>();
                }

                return flaskResponse.Recommendations;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

