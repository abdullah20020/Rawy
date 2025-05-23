using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rawy.Dtos
{
    public class AddReviewDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; } = 2;
        public string Comment { get; set; }
        
    }
}
