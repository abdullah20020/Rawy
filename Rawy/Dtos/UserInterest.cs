using core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class UserInterestDtos
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
  
    }
}
