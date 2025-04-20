using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;
using Rawy.Dtos.favoriteDtos;
using System.Security.Claims;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private IMapper mapper;
        private IGenaricrepostry<Record> genaricrepostry;

        public RecordController(IMapper mapper, IGenaricrepostry<Record> genaricrepostry)
        {
            this.mapper = mapper;
            this.genaricrepostry = genaricrepostry;

        }
        [HttpPost]

        public async Task<ActionResult<RecordDtos>> AddRecord([FromBody] RecordDtos dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var record = mapper.Map<RecordDtos, Record>(dto);

            record.BaseUserId = userId;

            var result = await genaricrepostry.set(record);

            var mapped = mapper.Map<Record, RecordDtos>(result);

            return Ok(mapped);
        }

 
        [HttpPut("make-recording-true/{id}")]
        public async Task<ActionResult> MakeRecordingTrue(int id)
        {
            var record = await genaricrepostry.GetByIdAsync(id);
            if (record == null) return NotFound("Record not found.");

            record.Okay_Record = true;

            await genaricrepostry.UpdateAsync(record);
            return Ok("Recording status set to true.");
        }


    }
}
