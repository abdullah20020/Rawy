namespace Rawy.Dtos.catygoryDtos
{
    public class CatygorybyidDtos
    {
        public string Name { get; set; }
        public ICollection <bookdtos> bookdtos { get; set; } = new HashSet<bookdtos>();
    }
}
