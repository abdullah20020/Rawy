using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class Playlist:BaseClass
    {

   
        
        [ForeignKey("BaseUserId")]
        public string BaseUserId { get; set; }

        
        public BaseUser User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public ICollection<Record> records { get; set; } = new HashSet<Record>();

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

 
    }
}
