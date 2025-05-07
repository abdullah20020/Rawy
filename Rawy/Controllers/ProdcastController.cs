using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;
using Repsotiry.spacification;

namespace Rawy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdcastController : ControllerBase
    {
        private readonly IGenaricrepostry<Prodcast> _repository;
        private readonly IMapper _mapper;

        public ProdcastController(IGenaricrepostry<Prodcast> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/prodcast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdcastDto>>> GetAll()
        {
            var spec = new ProdcastSpacificaton(); // تعديل التسمية هنا
            var prodcasts = await _repository.getallwithspacAsync(spec); // تعديل التسمية هنا
            return Ok(_mapper.Map<IEnumerable<ProdcastDto>>(prodcasts));
        }

        // GET: api/prodcast/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdcastDto>> GetById(int id)
        {
            var spec = new ProdcastSpacificaton(id); // تعديل التسمية هنا
            var prodcast = await _repository.getbyidwithspacAsync(spec); // تعديل التسمية هنا
            if (prodcast == null) return NotFound();
            return Ok(_mapper.Map<ProdcastDto>(prodcast));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProdcastDto dto)
        {
            var prodcast = _mapper.Map<Prodcast>(dto);
            var createdProdcast = await _repository.set(prodcast); // تغيير إلى AddAsync
            return CreatedAtAction(nameof(GetById), new { id = createdProdcast.Id }, _mapper.Map<ProdcastDto>(createdProdcast));
        }

        // PUT: api/prodcast/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProdcastDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing); // تغيير إلى UpdateAsync

            return NoContent();
        }

        // DELETE: api/prodcast/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var prodcast = await _repository.GetByIdAsync(id);
            if (prodcast == null) return NotFound();

            await _repository.DeleteAsync(prodcast); // تغيير إلى DeleteAsync
            return NoContent();
        }
    }
}
