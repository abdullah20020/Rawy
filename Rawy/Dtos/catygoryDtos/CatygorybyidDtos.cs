namespace Rawy.Dtos.catygoryDtos
{
    public class CatygorybyidDtos
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        public ICollection <bookdtos> bookdtos { get; set; } = new HashSet<bookdtos>();
    }
}
