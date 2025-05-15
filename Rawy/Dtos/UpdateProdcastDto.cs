using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class UpdateProdcastDto
    {
        public string? Prodcastname { get; set; }
        public string? Prodcastimage { get; set; }
        public string? BaseUserId { get; set; }
        public DateTime? ReleaseDate { get; set; }


        public List<int>? EpisodeIds { get; set; }
    }
    public class CreateProdcastDto
    {
        [Required]
        public string Prodcastname { get; set; }

        public string? Prodcastimage { get; set; }

        [Required]
        public string BaseUserId { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public List<int>? EpisodeIds { get; set; }
    }

}
