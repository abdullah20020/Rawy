using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rawy.Dtos
{
    public class AuthorDtos
    {   public int id { get; set; }
        public string Name { get; set; }
        
        public string Descriotion { get; set; }

        public string? ProfilePicture { get; set; }




    }
}
