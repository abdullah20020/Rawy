using System.ComponentModel.DataAnnotations;

namespace Rawy.Dtos
{
public class ReviewDto
{

        public string UserId { get; set; }
    public int? BookId { get; set; }
    public int?episodeId { get; set; }
    public int? ProdcastId { get; set; }
     
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime DatePosted { get; set; }
}
}
