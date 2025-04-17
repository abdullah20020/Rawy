using AutoMapper;
using core.Models;
using core.Prametars;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IMapper mapper;
        private IGenaricrepostry<Favorite> genaricrepostry;
        private IGenaricrepostry<Book> genaricrepostryb;

        public MyfavoriteController(IMapper mapper, IGenaricrepostry<Favorite> genaricrepostry,IGenaricrepostry<Book> genaricrepostryb)
        {
            this.genaricrepostryb = genaricrepostryb;
            this.mapper = mapper;
            this.genaricrepostry = genaricrepostry; 

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
        
        [HttpPost]
        public async Task<ActionResult<FavoriteDtos>> AddFavorite([FromBody] AddFavoriteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var favorite = mapper.Map<AddFavoriteDto, Favorite>(dto);

            favorite.BaseUserId = userId; 

            var books = await genaricrepostryb.GetBooksByIdsAsync(dto.BookIds);

            favorite.Books = books.ToList();

            var result = await genaricrepostry.set(favorite);

            var mapped = mapper.Map<Favorite, FavoriteDtos>(result);

            return Ok(mapped);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FavoriteDtos>> UpdateFavorite(int id, [FromBody] UpdateFavoriteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var favspac = new MyFavoriteSpacification(id);
            var favorite = await genaricrepostry.getbyidwithspacAsync(favspac);

            if (favorite == null)
                return NotFound("Favorite not found");

            if (!string.IsNullOrEmpty(dto.Name))
                favorite.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.BaseUserId))
                favorite.BaseUserId = dto.BaseUserId;

            if (dto.BookIds != null && dto.BookIds.Any())
            {
                favorite.Books.Clear();
                var books = await genaricrepostryb.GetBooksByIdsAsync(dto.BookIds);
                foreach (var book in books)
                {
                    favorite.Books.Add(book);
                }
            }

            var updatedFavorite = await genaricrepostry.UpdateAsync(favorite);

            var mapped = mapper.Map<Favorite, FavoriteDtos>(updatedFavorite);

            return Ok(mapped);
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
