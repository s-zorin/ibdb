using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Reviews.Application.Repositories;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class FindReviewQueryHandler : IQueryHandler<FindReviewQuery, ReviewDto?>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public FindReviewQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ReviewDto?> Handle(FindReviewQuery query)
        {
            var review = await _reviewRepository.Find(query.Id);
            return _mapper.Map<ReviewDto?>(review);
        }
    }
}
