using AutoMapper;
using core.Models;
using core.Prametars;
using core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rawy.Dtos;
using Repsotiry.Data;
using Repsotiry.GenaricReposiory;
using Repsotiry.spacification;
using Services;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {
        private readonly IGenaricrepostry<Book> genaricrepostry;
        private readonly IHubContext<NotificationHub> hubContext;
        private readonly IMapper mapper;

        private readonly IGenaricrepostry<Record> genaricrepostryrecords;
        private readonly RawyDbcontext rawyDbcontext;
        private readonly IMemoryCache memoryCache;

        public bookController(IGenaricrepostry<Book> genaricrepostry, IHubContext<NotificationHub> hubContext, IMapper mapper, IGenaricrepostry<Record> genaricrepostryrecords, RawyDbcontext rawyDbcontext, IMemoryCache memoryCache)
        {
            this.genaricrepostry = genaricrepostry;
            this.hubContext = hubContext;
            this.mapper = mapper;
            this.genaricrepostryrecords = genaricrepostryrecords;
            this.rawyDbcontext = rawyDbcontext;
            this.memoryCache = memoryCache;
        }
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable
        //<Book>>> getall()
        //{
        //    var books = await genaricrepostry.getallAsync();
        //    return Ok(books);

        //}

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<bookdtos>>> getallwithspac([FromQuery] Bookspecpram bookspecpram)
        {
            var spac = new bookspacefcation(bookspecpram);

            var books = await genaricrepostry.getallwithspacAsync(spac);
            var mappeing = mapper.Map<IReadOnlyList<Book>, IReadOnlyList<bookdtos>>(books);

            foreach (var book in mappeing)
            {
                book.RecordDtos = book.RecordDtos?.Where(r => r.IsRecording).ToList();
            }

            return Ok(mappeing);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<bookdtos>> getbyidwithspac(int id)
        {
            var spac = new bookspacefcation(id);
            var book = await genaricrepostry.getbyidwithspacAsync(spac);
            if (book == null) return NotFound();

            var mappeing = mapper.Map<Book, bookdtos>(book);


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                // تأكد إن ما فيش ريكورد سابق لنفس المستخدم ونفس الكتاب
                var exists = await rawyDbcontext.UserInterests
                    .AnyAsync(ui => ui.UserId == userId && ui.BookId == book.Id);

                if (!exists)
                {
                    var interest = new UserInterestbook
                    {
                        UserId = userId,
                        BookId = book.Id
                    };
                    rawyDbcontext.UserInterests.Add(interest);
                    await rawyDbcontext.SaveChangesAsync();
                }
            }

            return Ok(mappeing);
        }

        [HttpPost]
        public async Task<ActionResult<bookdtos>> CreateBook([FromBody] bookdtos bookDto)
        {
            var bookEntity = mapper.Map<Book>(bookDto);
            await genaricrepostry.set(bookEntity);

            await hubContext.Clients.All.SendAsync("ReceiveNotification", $"new book,We added : {bookDto.BookTitle}");
            return CreatedAtAction(nameof(getbyidwithspac), new { id = bookEntity.Id }, mapper.Map<bookdtos>(bookEntity));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] bookdtos bookDto)
        {
            var existingBook = await genaricrepostry.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            var updatedBook = mapper.Map(bookDto, existingBook);
            await genaricrepostry.UpdateAsync(updatedBook);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await genaricrepostry.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await genaricrepostry.DeleteAsync(book);
            return NoContent();
        }
        [HttpGet("recommendation")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<bookdtos>>> GetUserRecommendedBooks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing or invalid in the token.");
            }

            // التحقق من وجود التوصيات في الكاش
            if (!memoryCache.TryGetValue(userId, out List<int> recommendedBookIds))
            {
                return NotFound("No recommendations found. Please login again.");
            }

            var books = new List<bookdtos>();

            // جلب الكتب بناءً على التوصيات
            foreach (int id in recommendedBookIds)
            {
                var spec = new bookspacefcation(id);
                var book = await genaricrepostry.getbyidwithspacAsync(spec);
                if (book != null)
                {
                    books.Add(mapper.Map<Book, bookdtos>(book));
                }
            }

            return Ok(books);
        }
    }
}
