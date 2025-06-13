using core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class bookdtos
    {


        public int Id { get; set; }
        public string BookTitle { get; set; }

        public string book { get; set; }
        public int AurthorId { get; set; }

        [Required]
        public string Language { get; set; }

        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string CoverImage { get; set; }
        public List <CatygoryDtos > catygoriesname { get; set; }

        public List <ReviewDto> reviewsdtos { get; set; } = new List<ReviewDto>(); 
        public List <RecordDtos> RecordDtos { get; set; } = new List<RecordDtos>(); 

     public AuthorDtos Aurthorname { get; set; }
    }

    public class bookAdmindtos
    {


        public int Id { get; set; }
        public string BookTitle { get; set; }

        public string book { get; set; }
        public int AurthorId { get; set; }

        [Required]
        public string Language { get; set; }

        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string CoverImage { get; set; }
        public List<CatygoryDtos> catygoriesname { get; set; }

        public List<ReviewDto> reviewsdtos { get; set; } = new List<ReviewDto>();
        public List<RecordDtos> RecordDtos { get; set; } = new List<RecordDtos>();

        public AuthorDtos Aurthorname { get; set; }
    }

    public class bookWithAuthorCategoryDto
    {
        [Required]
        public string BookTitle { get; set; }

        [Required]
        public string AuthorName { get; set; }


        public List<int> CategoryIds { get; set; }

        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string Language { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
