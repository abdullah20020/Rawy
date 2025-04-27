using core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
    public class UserInterestDtos
    {
        
        public string UserId { get; set; }
        public int BookId { get; set; }
  
    }
}
