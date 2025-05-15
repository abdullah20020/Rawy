using System;
using System.ComponentModel.DataAnnotations;
using Rawy.Dtos.Base;

namespace Rawy.Dtos.Review
{
    public class ReviewDto : BaseDto
    {
        public string UserId { get; set; }
        
        public string UserDisplayName { get; set; }

        public int? BookId { get; set; }
        
        public int? EpisodeId { get; set; }
        
        public int? PodcastId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
    }

    public class CreateReviewDto
    {
        [Required]
        public int? BookId { get; set; }
        
        public int? EpisodeId { get; set; }
        
        public int? PodcastId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
    }

    public class UpdateReviewDto
    {
        [Range(1, 5)]
        public int? Rating { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }
    }
} 