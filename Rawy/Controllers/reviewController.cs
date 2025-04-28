using AutoMapper;
using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawy.Dtos;
using Repsotiry.GenaricReposiory;
using System.Security.Claims;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reviewController : ControllerBase
    {
        private IMapper _mapper;
        private IGenaricrepostry<Review> _genaricrepostry;
        private IGenaricrepostry<Book> _genaricrepostryb;

        public reviewController(IMapper mapper, IGenaricrepostry<Review> genaricrepostry, IGenaricrepostry<Book> genaricrepostryb)
        {
            this._genaricrepostryb = genaricrepostryb;
            this._mapper = mapper;
            this._genaricrepostry = genaricrepostry;

        }
        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _genaricrepostry.GetAllAsync();
            var reviewsDto = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _genaricrepostry.GetByIdAsync(id);
            //if (review == null)
            //{
            //    return NotFound();
            //}

            var reviewDto = _mapper.Map<Review, ReviewDto>(review);
            return Ok(reviewDto);
        }

        // POST: api/Reviews
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Review>> PostReview(AddReviewDto reviewDto)
        {

            var review = _mapper.Map<AddReviewDto, Review>(reviewDto);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
             review.UserId =  userId  ;
             
            }
              await _genaricrepostry.set(review);

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
 
        public async Task<IActionResult> PutReview(int id, AddReviewDto reviewDto)
        {
            var review = await _genaricrepostry.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _mapper.Map(reviewDto, review);
            await _genaricrepostry.UpdateAsync(review);

            return NoContent();
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _genaricrepostry.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _genaricrepostry.DeleteAsync(review);
            return NoContent();
        }


    }
}
