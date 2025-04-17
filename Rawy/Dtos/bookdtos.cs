using core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class bookdtos
    {


        public string BookTitle { get; set; }

        public string book { get; set; }
        public int AurthorId { get; set; }

        [Required]
        public string Language { get; set; }

        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string CoverImage { get; set; }
        public List <CatygoryDtos > catygoriesname { get; set; }

        public List <reviewsdto> reviewsdtos { get; set; } = new List<reviewsdto>(); 
        public List <RecordDtos> RecordDtos { get; set; } = new List<RecordDtos>(); 

     public AuthorDtos Aurthorname { get; set; }
    }
}
