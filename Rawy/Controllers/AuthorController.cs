using AutoMapper;
using core.Models;
using core.Prametars;
using core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;
using Repsotiry.spacification;
using static System.Reflection.Metadata.BlobBuilder;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IGenaricrepostry<Aurthor> genaricrepostry;
        private readonly IMapper mapper;

        private readonly IGenaricrepostry<Book> genaricrepostryBook;
        public AuthorController(IGenaricrepostry<Aurthor> genaricrepostry, IMapper mapper, IGenaricrepostry<Book> genaricrepostryBook)
        {
            this.genaricrepostry = genaricrepostry;
            this.mapper = mapper;
            this.genaricrepostryBook = genaricrepostryBook;
        }


        //// GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AuthorDtos>>> GetAllAuthors([FromQuery] Bookspecpram bookspecpram)
        {
            var Author = new AuthorSpecfication(bookspecpram);
            var authors = await genaricrepostry.getallwithspacAsync(Author);
            var authorDtos = mapper.Map<IReadOnlyList<Aurthor>, IReadOnlyList<AuthorDtos>>(authors);
            return Ok(authorDtos);
        }

        //// GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<authoridDtos>> GetAuthor(int id)
        {
            var Author = new AuthorSpecfication(id);
            var authors = await genaricrepostry.getbyidwithspacAsync(Author);
            if (authors == null) return NotFound();

            var authorDtos = mapper.Map<Aurthor, authoridDtos>(authors);
            return Ok(authorDtos);
        }

        //// POST: api/Author
        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromBody] AuthorDtos authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = mapper.Map<AuthorDtos, Aurthor>(authorDto);
            await genaricrepostry.set(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorDtos authorDto)
        {
            var spec = new AuthorSpecfication(id);
            var existingAuthor = await genaricrepostry.getbyidwithspacAsync(spec);

            if (existingAuthor == null)
                return NotFound();

         
            mapper.Map(authorDto, existingAuthor);

            await genaricrepostry.UpdateAsync(existingAuthor);

            return NoContent();
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var spec = new AuthorSpecfication(id);
            var existingAuthor = await genaricrepostry.getbyidwithspacAsync(spec);
            if (existingAuthor == null) return NotFound();

            await genaricrepostry.DeleteAsync(existingAuthor);
            return NoContent();
        }
    }
}

