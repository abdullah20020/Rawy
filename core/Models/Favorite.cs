using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Favorite:BaseClass
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

       
        public string BaseUserId { get; set; }

        [ForeignKey("BaseUserId")]
        public BaseUser User { get; set; }

    public ICollection<Book> Books { get;set; } = new HashSet<Book>();
    }
}
