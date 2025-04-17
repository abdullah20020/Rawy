using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;
using Repsotiry.spacification;

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


        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult> GetAllAuthors()
        {
            var Author = new AuthorSpecfication();
            var authors = await genaricrepostry.getallwithspacAsync(Author);
            var authorDtos = mapper.Map<IEnumerable<AuthorDtos>>(authors);
            return Ok(authorDtos);
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<AuthorDtos>>> GetAuthor(int id)
        {
            var author = await genaricrepostry.GetByIdAsync(id);
            if (author == null) return NotFound();

            var authorDto = mapper.Map<AuthorDtos>(author);
            return Ok(authorDto);
        }

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromBody] AuthorDtos authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = mapper.Map<Aurthor>(authorDto);
            await genaricrepostry.set(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorDtos authorDto)
        {
            var existingAuthor = await genaricrepostry.GetByIdAsync(id);
            if (existingAuthor == null) return NotFound();

            mapper.Map(authorDto, existingAuthor);
            await genaricrepostry.UpdateAsync(existingAuthor);
            return NoContent();
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await genaricrepostry.GetByIdAsync(id);
            if (author == null) return NotFound();

            await genaricrepostry.DeleteAsync(author);
            return NoContent();
        }
    }
}

