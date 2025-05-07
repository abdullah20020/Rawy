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
        public Record record { get; set; }
    }
}
