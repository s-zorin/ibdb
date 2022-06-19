using AutoMapper;
using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;

namespace Ibdb.Reviews.Infrastructure.MappingProfiles
{
    public class ReviewsMappingProfile : Profile
    {
        public ReviewsMappingProfile()
        {
            CreateMap<CreateReviewDto, CreateReviewCommand>();

            CreateMap<CreateReviewCommand, ReviewCreatedEventDataDto>();
        }
    }
}
