using AutoMapper;
using core.Models;
using Rawy.Dtos;

namespace Rawy.Helpers
{
    public class BookPdfUrl : IValueResolver<Book, bookdtos, string>
    {
        private readonly IConfiguration configuration;

        public BookPdfUrl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Book source, bookdtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.bookurl))
                return $"{configuration["ApiBaseUrl"]}{source.bookurl}";
            return string.Empty;
        }
    }
}
