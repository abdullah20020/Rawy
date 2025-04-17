using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class reviewsdto
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
    }
}
