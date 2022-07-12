using AutoMapper;
using Ibdb.Shared.Application.Dtos;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Shared.Infrastructure.MappingProfiles
{
    internal class ExceptionMappingProfile : Profile
    {
        public ExceptionMappingProfile()
        {
            CreateMap<Exception, ErrorDto>().ConstructUsing(Constructor);

            static ErrorDto Constructor(Exception e, ResolutionContext _) => e switch
            {
                ValidationException ve => new ErrorDto(ve.Code, ve.Message),
                BusinessException be => new ErrorDto(be.Code, be.Message),
                _ => new ErrorDto("INTERNAL_SERVER_ERROR", e.Message)
            };
        }
    }
}
