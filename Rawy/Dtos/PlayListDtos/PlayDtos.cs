using core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos.PlayListDtos
{
    public class PlayListDtos
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public List<RecordDtos> records { get; set; } 
        public DateTime DateCreated { get; set; } 
    }
}
