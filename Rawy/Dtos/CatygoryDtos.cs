using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class CatygoryDtos
    {

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }



    }
}
