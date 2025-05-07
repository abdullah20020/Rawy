using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class UserInterestbook:BaseClass
    {

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public BaseUser User { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }

        public Book Book { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

