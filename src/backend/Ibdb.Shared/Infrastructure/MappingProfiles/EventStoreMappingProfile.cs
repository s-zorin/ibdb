using AutoMapper;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Domain;

namespace Ibdb.Shared.Infrastructure.MappingProfiles
{
    internal class EventStoreMappingProfile : Profile
    {
        public EventStoreMappingProfile()
        {
            CreateMap<Event, EventDto>();
        }
    }
}
