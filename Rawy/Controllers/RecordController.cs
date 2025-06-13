using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rawy.Dtos;
using Rawy.Dtos.favoriteDtos;
using Repsotiry.Data;
using System.Net;
using System.Security.Claims;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private IMapper mapper;
        private IGenaricrepostry<Record> genaricrepostry;
        private readonly RawyDbcontext rawyDbcontext;

        public RecordController(IMapper mapper, IGenaricrepostry<Record> genaricrepostry,RawyDbcontext rawyDbcontext)
        {
            this.mapper = mapper;
            this.genaricrepostry = genaricrepostry;
            this.rawyDbcontext = rawyDbcontext;
        }
        //[HttpPost]

        //public async Task<ActionResult<RecordDtos>> AddRecord([FromBody] RecordDtos dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);


        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return Unauthorized("User is not authenticated.");
        //    }

        //    var record = mapper.Map<RecordDtos, Record>(dto);

        //    record.BaseUserId = userId;

        //    var result = await genaricrepostry.set(record);

        //    var mapped = mapper.Map<Record, RecordDtos>(result);

        //    return Ok(mapped);
        //}

 
        [HttpPut("make-recording-true/{id}")]
        public async Task<ActionResult> MakeRecordingTrue(int id)
        {
            var record = await genaricrepostry.GetByIdAsync(id);
            if (record == null) return NotFound("Record not found.");

            record.Okay_Record = true;

            await genaricrepostry.UpdateAsync(record);
            return Ok("Recording status set to true.");
        }


       [HttpGet("not-accepted-with-book")]
       public async Task<ActionResult<IEnumerable<RecordDtos>>> GetNotAcceptedRecords()
        {
            var records = await rawyDbcontext.Records
                .Where(r => r.Okay_Record == false && r.BookId != null)
                .ToListAsync();

            var mapped = mapper.Map<IEnumerable<RecordDtos>>(records);

            return Ok(mapped);
        } 

        [Authorize]
        [HttpPost("upload-record")]
        public async Task<IActionResult> UploadRecord([FromForm] uploadRecordDtos dto)
        {
            var file = dto.AudioFile;
            if (file == null || file.Length == 0)
                return BadRequest("Audio is not found ");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "audios");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated.");

            var record = new Record
            {
                AudioFile = uniqueFileName,
                BaseUserId = userId,
                BookId = dto.BookId,
                DatePosted = DateTime.UtcNow,
                //Okay_Record = false,
            };

            rawyDbcontext.Records.Add(record);
            await rawyDbcontext.SaveChangesAsync();

            return Ok(new
            {
                record.Id,
                FileUrl = $"{Request.Scheme}://{Request.Host}/uploads/audios/{uniqueFileName}"
            });
        }
    }
}
