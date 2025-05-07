namespace Rawy.Dtos.favoriteDtos
{

  
        public class UpdateFavoriteDto
        {
        public int Id { get; set; }

        public string? Name { get; set; }
            public string? BaseUserId { get; set; }
            public List<int> BookIds { get; set; }
        }
   
}
