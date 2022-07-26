using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Books.Application.Repositories;
using Ibdb.Shared.Application;

namespace Ibdb.Books.Application.Handlers
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
            var book = await _bookRepository.Find(query.Id);
            return _mapper.Map<BookDto?>(book);
        }
    }
}
