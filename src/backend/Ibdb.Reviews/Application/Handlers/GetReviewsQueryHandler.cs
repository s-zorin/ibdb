using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Reviews.Application.Repositories;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Reviews.Application.Handlers
{
    public class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, (ICollection<ReviewDto> Reviews, int TotalCount)>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GetReviewsQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<(ICollection<ReviewDto> Reviews, int TotalCount)> Handle(GetReviewsQuery query)
        {
            if (query.Skip < 0)
                throw new ValidationException("Can't skip less than zero reviews.", "REVIEWS_SKIP_BELOW_LIMIT");

            if (query.Take == 0)
                throw new ValidationException("Should take at least one review.", "REVIEWS_TAKE_BELOW_LIMIT");

            if (query.Take > 100)
                throw new ValidationException("Can't take more than 100 reviews.", "REVIEWS_TAKE_ABOVE_LIMIT");

            var reviews = await _reviewRepository.Get(query.Skip, query.Take);
            var reviewsCount = await _reviewRepository.Count();
            return (_mapper.Map<ICollection<ReviewDto>>(reviews), reviewsCount);
        }
    }
}
