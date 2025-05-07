using AutoMapper;
using core.Models;
using Rawy.Dtos;

namespace Rawy.Helpers
{
    public class AuthorPic : IValueResolver<Aurthor, AuthorDtos, string>
    {
        private readonly IConfiguration configuration;

        public AuthorPic(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Aurthor source, AuthorDtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProfilePicture))
                return $"{configuration["ApiBaseUrl"]}{source.ProfilePicture}";
            return string.Empty;
        }
    }
}
