using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class UserInterest:BaseClass
    {

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public BaseUser User { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Catygory Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

