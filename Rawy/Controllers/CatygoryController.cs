using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;
using Rawy.Dtos.catygoryDtos;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatygoryController : ControllerBase
    {
        private IMapper mapper;
        private IGenaricrepostry<Catygory> catygoryrepo;

        public CatygoryController(IMapper mapper, IGenaricrepostry<Catygory> catygoryrepo)
        {
            this.mapper = mapper;
            this.catygoryrepo = catygoryrepo;
          
        }

        [HttpGet("catygorys")]
        public async Task<ActionResult<IReadOnlyList<CatygoryDtos>>> Getallcatygory()
        {
            var Catygorys = await catygoryrepo.GetAllAsync();

            return Ok(mapper.Map<IReadOnlyList<Catygory>, IReadOnlyList<CatygoryDtos>>(Catygorys));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatygoryDtos>> GetCatygoryById(int id)
        {
            var category = await catygoryrepo.GetByIdAsync(id);

            if (category == null)
                return NotFound($"Catygory with id {id} not found.");

            var dto = mapper.Map<CatygoryDtos>(category);

            return Ok(dto);
        }

        [HttpPost("add")]
        public async Task<ActionResult<CatygoryDtos>> AddCatygory([FromBody] AddCatygoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var catygory = mapper.Map<AddCatygoryDto, Catygory>(dto);

            var result = await catygoryrepo.set(catygory);

            var mapped = mapper.Map<Catygory, CatygoryDtos>(result);

            return Ok(mapped);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatygoryDtos>> UpdateCatygory(int id, [FromBody] UpdateCatygoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await catygoryrepo.GetByIdAsync(id);

            if (existing == null)
                return NotFound("Category not found");

            if (!string.IsNullOrEmpty(dto.Name))
                existing.Type = dto.Name;

            var updated = await catygoryrepo.UpdateAsync(existing);

            var mapped = mapper.Map<Catygory, CatygoryDtos>(updated);

            return Ok(mapped);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCatygory(int id)
        {
            var existing = await catygoryrepo.GetByIdAsync(id);

            if (existing == null)
                return NotFound("Category not found");

            await catygoryrepo.DeleteAsync(existing);

            return NoContent(); 
        }
    }
}
