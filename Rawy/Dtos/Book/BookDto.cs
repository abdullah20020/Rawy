using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rawy.Dtos.Base;
using Rawy.Dtos.Review;


namespace Rawy.Dtos.Book
{
    public class BookDto
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

        public AuthorDtos Author { get; set; }

        public ICollection<CatygoryDtos> Categories { get; set; } = new List<CatygoryDtos>();

        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        public ICollection<RecordDtos> Records { get; set; } = new List<RecordDtos>();
    }
} 