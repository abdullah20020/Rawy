namespace Rawy.Dtos
{
    public class GetuserDtos
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }
        public string Email { get; set; }
         
        public string Phone { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string? Cv_Url { get; set; }
        public string Password { get; set; }


        public string Token { get; set; }

    }
}
