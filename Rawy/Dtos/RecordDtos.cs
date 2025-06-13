using core.Models;
using System;

namespace Rawy.Dtos
{
    public class RecordDtos
    {
        public int Id { get; set; }
        public string AudioFile { get; set; }

        public bool IsRecording { get; set; } = false;
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public int? bookId { get; set; }
        public int? episodeId { get; set; }
     

    }


    public class uploadRecordDtos
    {
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public int BookId { get; set; }
        public IFormFile AudioFile { get; set; } 
    }
}
