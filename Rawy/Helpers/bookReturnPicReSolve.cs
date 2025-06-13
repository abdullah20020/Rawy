using AutoMapper;
using core.Models;
using Rawy.Dtos;

namespace Rawy.Helpers
{
    public class bookReturnPicReSolve : IValueResolver<Book, bookdtos, string>
    {
        private readonly IConfiguration configuration;

        public bookReturnPicReSolve(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Book source, bookdtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.CoverImage))
                return $"{configuration["ApiBaseUrl"]}{source.CoverImage}";
            return string.Empty;
        }
    }
}
