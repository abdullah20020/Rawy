using core.Models;

namespace Rawy.Dtos
{
    public class UserAccountDto
    {

        public string email { get; set; }
        public string? ProfilePicture { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string? Cv_Url { get; set; }
        public ICollection<Record>? Records { get; set; } 
        public ICollection<Playlist>? Playlists { get; set; }
        public ICollection<core.Models.Review>? Reviews { get; set; } 
        public ICollection<Favorite>? Favorites { get; set; }
    }
}
