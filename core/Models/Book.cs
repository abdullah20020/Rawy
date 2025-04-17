using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Book:BaseClass
    {

  
        [MaxLength(200)]
        public string BookTitle { get; set; }

        public string CoverImage { get; set; }


        public string bookurl { get; set; }
  
   
        public string Language { get; set; }

        public DateTime? ReleaseDate { get; set; }

      

        public ICollection<Review> reviews { get; set; } = new HashSet<Review>();   
     
   
        public ICollection<Catygory> catygories { get; set; } = new HashSet<Catygory>();

        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
      
       public ICollection<Record> record { get; set; } =new HashSet<Record>();
        [Required]
        [ForeignKey("AurthorId")]
        public int AurthorId { get; set; }
       
        [Required]
        public Aurthor Aurthor { get; set; }



    }
}
