using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Reviews.Repositories;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class GetBookReviewsQueryHandler : IQueryHandler<GetBookReviewsQuery, ICollection<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GetBookReviewsQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReviewDto>> Handle(GetBookReviewsQuery query)
        {
            var reviews = await _reviewRepository.GetByBookId(query.BookId);
            return _mapper.Map<ICollection<ReviewDto>>(reviews);
        }
    }
}
