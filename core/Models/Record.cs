using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace core.Models
{
    public class Record : BaseClass
    {
        [Required]
        public string AudioFile { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
         public bool Okay_Record { get; set; } = false;
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string? BaseUserId { get; set; }
        public BaseUser? User { get; set; }

        public ICollection<Playlist>? Playlists { get; set; }= new HashSet<Playlist>();
    }

}
