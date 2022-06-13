using AutoMapper;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Domain;

namespace Ibdb.Shared.Infrastructure.MappingProfiles
{
    internal class OutboxMappingProfile : Profile
    {
        public OutboxMappingProfile()
        {
            CreateMap<OutboxEvent, EventDto>();
        }
    }
}
