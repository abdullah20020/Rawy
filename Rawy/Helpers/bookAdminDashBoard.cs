using AutoMapper;
using core.Models;
using Rawy.Dtos;

namespace Rawy.Helpers
{
    public class bookAdminDashBoard : IValueResolver<Book, bookAdmindtos, string>
    {
        private readonly IConfiguration configuration;

        public bookAdminDashBoard(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Book source, bookAdmindtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.CoverImage))
                return $"{configuration["ApiBaseUrl"]}{source.CoverImage}";
            return string.Empty;
        }
    }
}
