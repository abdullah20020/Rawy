using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Prodcast : BaseClass
    {
        public string  Prodcastname { get; set; }

        public string? Prodcastimage { get; set; }

        public string? BaseUserId { get; set; }
        public BaseUser? User { get; set; }


        public DateTime? ReleaseDate { get; set; }



        public ICollection<Review> reviews { get; set; } = new HashSet<Review>();

       public ICollection<Playlist> Playlist { get; set; } =new HashSet<Playlist>();    
        public ICollection<episode> episode { get; set; } = new HashSet<episode>();
    }
}
