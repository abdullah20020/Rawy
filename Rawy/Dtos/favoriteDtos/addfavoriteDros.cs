using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos.favoriteDtos
{
    public class AddFavoriteDto
    {
        [Required]
        public string Name { get; set; }
        public List<int> BookIds { get; set; } 

    }
}
