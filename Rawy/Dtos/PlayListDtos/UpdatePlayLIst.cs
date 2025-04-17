namespace Rawy.Dtos.PlayListDtos
{
    public class UpdatePlayListDtos
    {
        public string Name { get; set; }

        public ICollection<int> RecordIds { get; set; }
    }
}
