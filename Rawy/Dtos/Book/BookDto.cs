using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rawy.Dtos.Base;
using Rawy.Dtos.Category;
using Rawy.Dtos.Review;
using Rawy.Dtos.Record;

namespace Rawy.Dtos.Book
{
    public class BookDto : BaseDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        public string CoverImageUrl { get; set; }

        public AuthorDto Author { get; set; }

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        public ICollection<RecordDto> Records { get; set; } = new List<RecordDto>();
    }
} 