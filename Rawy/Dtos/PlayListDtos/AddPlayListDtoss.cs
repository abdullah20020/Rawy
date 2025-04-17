using core.Models;

namespace Rawy.Dtos.PlayListDtos
{
    public class AddPlayListDtoss
    {
        public string Name { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public ICollection<int> RecordIds { get; set; }
    }
}
