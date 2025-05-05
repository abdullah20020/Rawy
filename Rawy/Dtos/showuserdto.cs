using core.Models;

namespace Rawy.Dtos
{
    public class showuserdto
    {
        public string Id { get; set; }
        public string email { get; set; }
        public int favoriteCount { get; set; }

       public ICollection<Record> records { get; set; }  = new List<Record>(); 
        public string? ProfilePicture { get; set; }
        public string DisplayName { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string? Cv_Url { get; set; }
        public int ReviewsCount { get; set; }
         
    }
}
