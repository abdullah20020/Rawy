using AutoMapper;
using core.Models;
using core.Prametars;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rawy.Dtos;
using Rawy.Dtos.favoriteDtos;
using Repsotiry.Data;
using Repsotiry.spacification;
using System.Security.Claims;


namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class MyfavoriteController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IGenaricrepostry<Favorite> genaricrepostry;
        private readonly IGenaricrepostry<Book> genaricrepostryb;
        private readonly RawyDbcontext _dbContext;

        public MyfavoriteController(
            IMapper mapper,
            IGenaricrepostry<Favorite> genaricrepostry,
            IGenaricrepostry<Book> genaricrepostryb,
            RawyDbcontext _dbcontext)
        {
            this.mapper = mapper;
            this.genaricrepostry = genaricrepostry;
            this.genaricrepostryb = genaricrepostryb;
            this._dbContext = _dbcontext;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<FavoriteDtos>>> GetallFavorites()
        {
   
            var fav = new MyFavoriteSpacification();
            var favorites = await genaricrepostry.getallwithspacAsync(fav);

            return Ok(mapper.Map<IReadOnlyList<Favorite>, IReadOnlyList<FavoriteDtos>>(favorites));

        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteDtos>> getbyidwithspacFavorite(int id)
        {
            var favspac = new MyFavoriteSpacification(id);
            var favorite = await genaricrepostry.getbyidwithspacAsync(favspac);
            var mappeing = mapper.Map<Favorite, FavoriteDtos>(favorite);

            return Ok(mappeing);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookToFavorites([FromBody] UpdateFavoriteDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var favorite = await _dbContext.Favorites
                .Include(f => f.Books)
                .FirstOrDefaultAsync(f => f.BaseUserId == userId);

            if (favorite == null)
            {
                favorite = new Favorite
                {
                    BaseUserId = userId,
                    Books = new List<Book>()
                };
                _dbContext.Favorites.Add(favorite);
            }

            var book = await _dbContext.Books.FindAsync(dto.BookId);
            if (book == null)
                return NotFound("Book not found");

            if (favorite.Books.Any(b => b.Id == book.Id))
            {
                return BadRequest("Book is already in favorites.");
            }
            else
            {
                favorite.Books.Add(book);
            }

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Book added to favorites." });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFavorite(int id)
        {
        
            var favspac = new MyFavoriteSpacification(id);
            var favorite = await genaricrepostry.getbyidwithspacAsync(favspac);

            if (favorite == null)
            {
                return NotFound("Favorite not found"); 
            }


            await genaricrepostry.DeleteAsync(favorite);

            return NoContent(); 
        }


    }
}
