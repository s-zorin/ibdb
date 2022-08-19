using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Reviews.Repositories;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class FindBookQueryHandler : IQueryHandler<FindBookQuery, BookDto?>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public FindBookQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<BookDto?> Handle(FindBookQuery query)
        {
            var book = await _reviewRepository.FindBook(query.BookId);
            return _mapper.Map<BookDto?>(book);
        }
    }
}
