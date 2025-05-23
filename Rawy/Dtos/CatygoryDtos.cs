using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class CatygoryDtos
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Type { get; set; }



    }
}
