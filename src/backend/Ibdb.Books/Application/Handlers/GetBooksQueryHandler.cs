using Ibdb.Books.Application.Dtos;
using Ibdb.Books.Application.Queries;
using Ibdb.Books.Application.Repositories;
using Ibdb.Shared.Application;
using Ibdb.Shared.Application.Exceptions;

namespace Ibdb.Books.Application.Handlers
{
    public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, (ICollection<BookDto> Books, int TotalCount)>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<(ICollection<BookDto> Books, int TotalCount)> Handle(GetBooksQuery query)
        {
            if (query.Skip < 0)
                throw new ValidationException("Can't skip less than zero books.", "BOOKS_SKIP_BELOW_LIMIT");

            if (query.Take == 0)
                throw new ValidationException("Should take at least one book.", "BOOKS_TAKE_BELOW_LIMIT");

            if (query.Take > 100)
                throw new ValidationException("Can't take more than 100 books.", "BOOKS_TAKE_ABOVE_LIMIT");

            var books = await _bookRepository.Get(query.Skip, query.Take);
            var booksCount = await _bookRepository.Count();
            return (_mapper.Map<ICollection<BookDto>>(books), booksCount);
        }
    }
}
