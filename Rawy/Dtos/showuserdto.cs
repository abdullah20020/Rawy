using core.Models;
using Rawy.Dtos.favoriteDtos;

namespace Rawy.Dtos
{
    public class showuserdto
    {
        public string Id { get; set; }
        public string email { get; set; }

        public string? ProfilePicture { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string? Cv_Url { get; set; }
        public  FavoriteDtos  favorites { get; set; }
        public ICollection<ProdcastDto>  prodcast { get; set; }   
         
    }
}
