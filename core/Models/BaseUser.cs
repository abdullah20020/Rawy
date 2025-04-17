using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class BaseUser: IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string? Cv_Url { get; set; }
        public ICollection<Record>? Records { get; set; } = new HashSet<Record>();
        public ICollection<Playlist>? Playlists { get; set; } = new HashSet<Playlist>();
        public ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    }
}
