namespace Rawy.Dtos.PlayListDtos
{
    public class UpdatePlayListDtos
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<int> RecordIds { get; set; }
    }
}
