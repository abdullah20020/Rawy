namespace core.Models
{
    public class Catygory:BaseClass
    {

        public string Type { get; set; }

        public ICollection<Book> books { get; set; } = new HashSet<Book>();
    }
}