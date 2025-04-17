using core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos.favoriteDtos
{
    public class FavoriteDtos
    {

        public string Name { get; set; }
        public ICollection<bookdtos> Books { get; set; } = new HashSet<bookdtos>();
    }
}
