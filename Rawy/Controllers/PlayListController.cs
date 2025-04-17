using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos.favoriteDtos;
using Rawy.Dtos.PlayListDtos;
using Repsotiry.GenaricReposiory;
using Repsotiry.spacification;
using System.Security.Claims;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PlayListController : ControllerBase
    {
        private IMapper mapper;
        private IGenaricrepostry<Playlist> genaricrepostry;
        private IGenaricrepostry<Record> genaricrepostryR;

        public PlayListController(IMapper mapper, IGenaricrepostry<Playlist> genaricrepostry, IGenaricrepostry<Record> genaricrepostryR)
        {
            this.mapper = mapper;
            this.genaricrepostry = genaricrepostry;
            this.genaricrepostryR = genaricrepostryR;

        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PlayListDtos>>> GetallFavorites()
        {

            var PLaylist = new PlayListSpecification();
            var getPL = await genaricrepostry.getallwithspacAsync(PLaylist);

            return Ok(mapper.Map<IReadOnlyList<Playlist>, IReadOnlyList<PlayListDtos>>(getPL));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayListDtos>> getbyidwithspacPLayList(int id)
        {
            var PlayList = new PlayListSpecification(id);
            var GetPLayList = await genaricrepostry.getbyidwithspacAsync(PlayList);
            return Ok(mapper.Map<Playlist, PlayListDtos>(GetPLayList));
        }

        [HttpPost]
        public async Task<ActionResult<PlayListDtos>> AddPlayList([FromBody] AddPlayListDtoss dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var PlayList = mapper.Map<AddPlayListDtoss, Playlist>(dto);

            PlayList.BaseUserId = userId;

            var record = await genaricrepostryR.GetBooksByIdsAsync(dto.RecordIds);

            PlayList.records = record.ToList();

            var result = await genaricrepostry.set(PlayList);



            var mapped = mapper.Map<Playlist, PlayListDtos>(result);

            return Ok(mapped);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlayListDtos>> UpdatePlayList(int id, [FromBody] UpdatePlayListDtos dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var playList = new PlayListSpecification(id);
            var getPL = await genaricrepostry.getbyidwithspacAsync(playList);

            if (getPL == null)
                return NotFound("Playlist not found");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            getPL.BaseUserId = userId;
           
            if (!string.IsNullOrEmpty(dto.Name))
                getPL.Name = dto.Name;

       
            if (dto.RecordIds != null && dto.RecordIds.Any())
            {
                getPL.records.Clear();

                var records = await genaricrepostryR.GetBooksByIdsAsync(dto.RecordIds);

                if (records == null || !records.Any())
                    return BadRequest("Invalid RecordIds.");

                foreach (var record in records)
                {
                    getPL.records.Add(record);
                }
            }

            var playlistUpdate = await genaricrepostry.UpdateAsync(getPL);
            var mapped = mapper.Map<Playlist, PlayListDtos>(playlistUpdate);

            return Ok(mapped);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleteplaylist(int id)
        {

            var Playspac = new PlayListSpecification(id);
            var playlist = await genaricrepostry.getbyidwithspacAsync(Playspac);

            if (playlist == null)
            {
                return NotFound("PlayList is not found");
            }


            await genaricrepostry.DeleteAsync(playlist);

            return NoContent();
        }
    }
}
