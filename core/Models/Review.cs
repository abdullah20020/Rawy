using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Review:BaseClass

    {
   
        [ForeignKey("UserId")]
        [Required]
        public string? UserId { get; set; }
        
        public BaseUser? User { get; set; }

        public int? BookId { get; set; }

        [ForeignKey("BookId")]
        public Book? Books { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; } 

        public string? Comment { get; set; }

        public DateTime? DatePosted { get; set; } = DateTime.UtcNow;


    }
}
