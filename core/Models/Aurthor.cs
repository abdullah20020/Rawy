using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Aurthor:BaseClass
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Descriotion { get; set; }

        public string? ProfilePicture { get; set; }

        public ICollection<Book> Books { get; set; }=new HashSet<Book>();



    }
}
