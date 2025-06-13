//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using core.Models;
//using System.Security.Claims;
//using Repsotiry.Data;
//using Repsotiry.GenaricReposiory;
//using Rawy.Dtos;
//using AutoMapper;
//using Services;

//namespace Rawy.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize]
//    public class UserInterestController : ControllerBase
//    {


//        private readonly RawyDbcontext rawyDbcontext;
//        private readonly IMapper mapper;
//        private readonly CsvGeneratorService csvService;

//        public UserInterestController(RawyDbcontext rawyDbcontext, IMapper mapper, CsvGeneratorService csvService)
//        {

//            this.rawyDbcontext = rawyDbcontext;
//            this.mapper = mapper;
//            this.csvService = csvService;
//        }


//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserInterestbook>>> GetMyInterests()
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            var interests = await rawyDbcontext.UserInterests
//                .Include(u => u.Book)
//                .Where(i => i.UserId == userId)
//                .ToListAsync();
//            var mappeing = mapper.Map<IEnumerable<UserInterestbook>, IEnumerable<UserInterestDtos>>(interests);

//            var memoryStream = csvService.GenerateCsvInMemory(mappeing);

//            await csvService.SendCsvToPythonServerAsync(memoryStream);

//            return Ok(mappeing);
//        }


//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteInterest(int id)
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            var interest = await rawyDbcontext.UserInterests
//                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

//            if (interest == null)
//                return NotFound();

//            rawyDbcontext.UserInterests.Remove(interest);
//            await rawyDbcontext.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}