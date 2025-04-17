using core.Models;

namespace Rawy.Dtos
{
    public class RecordDtos
    {
        public string AudioFile { get; set; }
        public string? ProfilePicture { get; set; }

        public bool IsRecording { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public int? bookId { get; set; }
     

    }
}
