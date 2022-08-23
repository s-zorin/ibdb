using AutoMapper;
using Ibdb.Reviews.Application.Commands;
using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Reviews.Domain;

namespace Ibdb.Reviews.Infrastructure.MappingProfiles
{
    public class ReviewsMappingProfile : Profile
    {
        public ReviewsMappingProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<Review, ReviewDto>();

            CreateMap<CreateReviewDto, CreateReviewCommand>();

            CreateMap<EditReviewDto, EditReviewCommand>();

            CreateMap<GetReviewsDto, GetReviewsQuery>();

            CreateMap<EditReviewDto, EditReviewCommand>();

            CreateMap<CreateReviewCommand, ReviewCreatedEventDataDto>();

            CreateMap<EditReviewCommand, ReviewEditedEventDataDto>();
        }
    }
}
