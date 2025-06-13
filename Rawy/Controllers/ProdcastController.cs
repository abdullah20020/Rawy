using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rawy.Dtos;
using Repsotiry.Data;
using Repsotiry.spacification;
using System.Security.Claims;

namespace Rawy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdcastController : ControllerBase
    {
        private readonly IGenaricrepostry<Prodcast> _repository;
        private readonly IMapper _mapper;
        private readonly RawyDbcontext rawyDbcontext;

        public ProdcastController(IGenaricrepostry<Prodcast> repository, IMapper mapper,RawyDbcontext rawyDbcontext)
        {
            _repository = repository;
            _mapper = mapper;
            this.rawyDbcontext = rawyDbcontext;
        }

        // GET: api/prodcast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdcastDto>>> GetAll()
        {
            var spec = new ProdcastSpacificaton();
            var prodcasts = await _repository.getallwithspacAsync(spec);
            return Ok(_mapper.Map<IEnumerable<ProdcastDto>>(prodcasts));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProdcastDto>> GetById(int id)
        {
            var spec = new ProdcastSpacificaton(id); 
            var prodcast = await _repository.getbyidwithspacAsync(spec); 
            if (prodcast == null) return NotFound();
            return Ok(_mapper.Map<ProdcastDto>(prodcast));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProdcastDto dto)
        {
            var prodcast = new Prodcast
            {
                Prodcastname = dto.Prodcastname,
                BaseUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                ReleaseDate = dto.ReleaseDate,
            };

            if (dto.Prodcastimage != null)
            {
               
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Prodcastimage.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await dto.Prodcastimage.CopyToAsync(stream);
                }

    
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                prodcast.Prodcastimage = imageUrl;
            }

           
            var created = await _repository.set(prodcast);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ProdcastDto>(created));
        }

        //// PUT: api/prodcast/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] UpdateProdcastDto dto)
        //{
        //    var existing = await _repository.GetByIdAsync(id);
        //    if (existing == null) return NotFound();

        //    _mapper.Map(dto, existing);
        //    await _repository.UpdateAsync(existing); // تغيير إلى UpdateAsync

        //    return NoContent();
        //}

        //// DELETE: api/prodcast/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var prodcast = await _repository.GetByIdAsync(id);
        //    if (prodcast == null) return NotFound();

        //    await _repository.DeleteAsync(prodcast); // تغيير إلى DeleteAsync
        //    return NoContent();
        //}
        [Authorize]
        [HttpPost("upload-episode")]
        public async Task<IActionResult> UploadEpisode([FromForm] uploadEpisodeDto dto)
        {
            var file = dto.record?.AudioFile;
            if (file == null || file.Length == 0)
                return BadRequest("prodcast is not here");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var episode = new episode
            {
                Title = dto.Title,
                ProdcastId = dto.ProdcastId,
                Date = DateTime.UtcNow,
                record = new Record
                {
                    AudioFile = uniqueFileName,
                    DatePosted = DateTime.UtcNow,
                    Okay_Record = false,
                }
            };

            rawyDbcontext.episodes.Add(episode);
            await rawyDbcontext.SaveChangesAsync();

            return Ok(new
            {
                episode.Id,
                episode.Title,
                FileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{uniqueFileName}"
            });
        }
    }
}
