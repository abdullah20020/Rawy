using AutoMapper;
using core.Models;
using Rawy.Dtos;

namespace Rawy.Helpers
{
    public class RecordAudioscs : IValueResolver<Record, RecordDtos, string>
    {
        private readonly IConfiguration configuration;

        public RecordAudioscs(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Record source, RecordDtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.AudioFile))
                return $"{configuration["ApiBaseUrl"]}{source.AudioFile}";
            return string.Empty;
        }
    }
}
