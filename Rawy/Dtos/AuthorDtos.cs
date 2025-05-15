using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using core.Models;

namespace Rawy.Dtos
{
    public class AuthorDtos
    {

        public string Name { get; set; }
        
        public string Descriotion { get; set; }

        public string? ProfilePicture { get; set; }

    }

    public class authoridDtos {

        public string Name { get; set; }

        public string Descriotion { get; set; }

        public string? ProfilePicture { get; set; }

        public ICollection<bookdtos> Books { get; set; } = new HashSet<bookdtos>();
    }

}

