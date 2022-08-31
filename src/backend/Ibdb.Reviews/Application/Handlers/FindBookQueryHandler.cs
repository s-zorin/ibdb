using Ibdb.Reviews.Application.Dtos;
using Ibdb.Reviews.Application.Queries;
using Ibdb.Reviews.Application.Repositories;
using Ibdb.Shared.Application;

namespace Ibdb.Reviews.Application.Handlers
{
    public class FindBookQueryHandler : IQueryHandler<FindBookQuery, BookDto?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public FindBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto?> Handle(FindBookQuery query)
        {
            var book = await _bookRepository.Find(query.BookId);
            return _mapper.Map<BookDto?>(book);
        }
    }
}
