using core.Models;

namespace Rawy.Dtos
{
    public class ProdcastDto
    {
 
        public string Prodcastname { get; set; }

        public string? Prodcastimage { get; set; }

        public string? BaseUserId { get; set; }


        public DateTime? ReleaseDate { get; set; }



        public ICollection<ReviewDto> reviews { get; set; } = new HashSet<ReviewDto>();

        public ICollection<EpisodeDto> episodeDtos { get; set; } = new HashSet<EpisodeDto>();
    }
}
