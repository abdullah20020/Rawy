using core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rawy.Dtos
{

    public class EpisodeDto 
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public RecordDtos record { get; set; }
    }
    public class episodeRecordDtos
    {
        public IFormFile AudioFile { get; set; }
    }

    public class uploadEpisodeDto
    {

        public string Title { get; set; }
        public int ProdcastId { get; set; } 
        public episodeRecordDtos record { get; set; }
    }

}
