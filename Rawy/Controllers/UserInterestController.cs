using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using core.Models;
using System.Security.Claims;
using Repsotiry.Data;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // لازم اليوزر يكون مسجل دخول
    public class UserInterestController : ControllerBase
    {
        private readonly RawyDbcontext rawycontext;

        public UserInterestController(RawyDbcontext rawycontext)
        {
          this.rawycontext = rawycontext;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInterest>>> GetMyInterests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var interests = await rawycontext.UserInterests
                .Include(u => u.Category)
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return Ok(interests);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var interest = await rawycontext.UserInterests
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (interest == null)
                return NotFound();

            rawycontext.UserInterests.Remove(interest);
            await rawycontext.SaveChangesAsync();

            return NoContent();
        }
    }
}